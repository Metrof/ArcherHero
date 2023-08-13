using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon
{
    private CancellationTokenSource _cancellationTokenSource;

    public ProjectilePattern ProjectilePattern { get; private set; }

    public Weapon(ObjectPool<Projectile> defaultProjectiles)
    {
        ProjectilePattern = new ProjectilePattern(defaultProjectiles);
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
        if(projectiles == null)
        {
            throw new ArgumentNullException(nameof(projectiles));
        }

        ProjectilePattern.ProjectilePool = projectiles;
    }

    private async UniTaskVoid InstantiateProjectileAsync(int attackSpeedPerMinute, int damage, Transform pointSpawnProjectile, Func<Transform> getTarget, CancellationToken token)
    {
        Transform currentTarget = getTarget();

        if (currentTarget == null)
        {
            return;
        }

        while (!token.IsCancellationRequested)
        {
            if (await UniTask.Delay(TimeSpan.FromMinutes(ShotDelay(attackSpeedPerMinute)), cancellationToken: token).SuppressCancellationThrow())
            {
                return;
            }

            currentTarget ??= getTarget();

            if (currentTarget == null)
            {
                StopAttack();
                return;
            }

            ProjectilePattern.Create(pointSpawnProjectile, currentTarget, damage);
        }
    }

    private double ShotDelay(float attackSpeedPerMinute)
    {
        if (attackSpeedPerMinute <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(attackSpeedPerMinute));
        }
        return 1 / attackSpeedPerMinute;
    }
}
