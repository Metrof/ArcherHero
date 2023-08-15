using System.Collections.Generic;
using UnityEngine;

public class EightAroundProjectileHit : IProjectileHit
{
    private EightCreatedProjectilesAround _eightProjectiles = new();
    public void Hit(Collider collider, Projectile projectile)
    {
        if (collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(projectile.TypeDamage, projectile.Damage);
        }

        Vector3 spawnPosition = projectile.transform.position;
        spawnPosition.y = projectile.StartPositionY;
        spawnPosition += projectile.transform.forward * projectile.GetComponent<SphereCollider>().radius * 2;

        List<Projectile> projectiles = _eightProjectiles.Create(spawnPosition, collider.transform, projectile.ProjectilePool);

        foreach (var bullet in projectiles)
        {
            bullet.Initialize(projectile.Damage, collider.transform, new DefaultProjectileMovement(), new DefaultProjectileHit());
            bullet.Move();
        }

        projectile.ProjectilePool.Release(projectile);
    }
}
