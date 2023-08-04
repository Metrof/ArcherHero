using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

public class Weapon
{
    private Projectile _currentProjectile;

    private CancellationTokenSource _cancellationTokenSource;

    private IProjectileMovement _currentMovement;

    public Weapon(Projectile defaultProjectile)
    {
        _currentProjectile = defaultProjectile;
        _currentMovement = new DefaultProjectileMovement();
    }

    public void StartAttack(Transform target, Transform pointSpawnProjectile, int damage, int attackSpeedPerMinute)
    {
        _cancellationTokenSource = new CancellationTokenSource();

        _ = InstantiateProjectileAsync(attackSpeedPerMinute, damage, pointSpawnProjectile, target, _cancellationTokenSource.Token);
    }

    public void StopAttack()
    {
        _cancellationTokenSource.Cancel();
    }

    public void ChangeProjectile(Projectile projectile)
    {
        _currentProjectile = projectile;
    }

    private async UniTaskVoid InstantiateProjectileAsync(int attackSpeedPerMinute, int damage, Transform pointSpawnSpell, Transform target, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            Projectile newProjectile = GameObject.Instantiate(_currentProjectile, pointSpawnSpell.position, pointSpawnSpell.rotation, null);
            newProjectile.Initialize(damage, _currentMovement, target.position);
            SetRotation(target, newProjectile);

            await UniTask.Delay(TimeSpan.FromMinutes(ShotDelay(attackSpeedPerMinute)), cancellationToken: token).SuppressCancellationThrow();
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
