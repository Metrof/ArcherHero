using System.Collections.Generic;
using UnityEngine;

public class EightAroundProjectileHit : IProjectileHit
{
    private EightCreatedProjectilesAround _eightCreatedProjectilesAround = new();
    public void Hit(Collider collider, Projectile projectile)
    {
        if (collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(projectile.TypeDamage, projectile.Damage);

            Vector3 spawnPosition = CalculatePosition(collider, projectile.transform.position.y);

            List<Projectile> projectiles = _eightCreatedProjectilesAround.Create(spawnPosition, projectile.transform, projectile.ProjectilePool);

            foreach (var bullet in projectiles)
            {
                bullet.Initialize(projectile.Damage, collider.transform, new DefaultProjectileMovement(), new DefaultProjectileHit());
            }
        }

        projectile.ProjectilePool.Release(projectile);
    }

    private Vector3 CalculatePosition(Collider collider, float Y)
    {
        Vector3 position = collider.transform.position;
        position.y = Y;
        return position;
    }
}
