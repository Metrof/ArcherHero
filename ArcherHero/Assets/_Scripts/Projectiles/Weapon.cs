using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon
{
    private ObjectPool<Projectile> _currentProjectiles;

    private CancellationTokenSource _cancellationTokenSource;

    private IProjectileMovement _currentMovement;

    public Weapon(ObjectPool<Projectile> defaultProjectiles)
    {
        _currentProjectiles = defaultProjectiles;
        _currentMovement = new DefaultProjectileMovement();
    }

    public void StartAttack(Func<Transform> nearestTarget, Transform pointSpawnProjectile, int damage, int attackSpeedPerMinute)
    {
        _cancellationTokenSource = new CancellationTokenSource();

        _ = InstantiateProjectileAsync(attackSpeedPerMinute, damage, pointSpawnProjectile, nearestTarget, _cancellationTokenSource.Token);
    }

    public void StopAttack()
    {
        _cancellationTokenSource?.Cancel();
    }

    public void ChangeProjectiles(ObjectPool<Projectile> projectiles)
    {
        _currentProjectiles = projectiles;
    }

    private async UniTaskVoid InstantiateProjectileAsync(int attackSpeedPerMinute, int damage, Transform pointSpawnSpell, Func<Transform> getTarget, CancellationToken token)
    {
        Transform currentTarget = getTarget();

        if (currentTarget == null)
        {
            return;
        }

        while (!token.IsCancellationRequested)
        {
            Projectile newProjectile = _currentProjectiles.Get();
            newProjectile.transform.position = pointSpawnSpell.position;
            newProjectile.Initialize(damage, _currentMovement, currentTarget.position);
            SetRotation(currentTarget, newProjectile);

            if(await UniTask.Delay(TimeSpan.FromMinutes(ShotDelay(attackSpeedPerMinute)), cancellationToken: token).SuppressCancellationThrow())
            {
                return;
            }

            currentTarget ??= getTarget();

            if(currentTarget == null)
            {
                StopAttack();
            }
        }
    }

    private void SetRotation(Transform target, Projectile newProjectile)
    {
        Vector3 targetPosition = target.position;
        targetPosition.y = newProjectile.transform.position.y;
        newProjectile.transform.LookAt(targetPosition);
    }

    private double ShotDelay(float attackSpeedPerMinute)
    {
        return 1 / attackSpeedPerMinute;
    }
}
