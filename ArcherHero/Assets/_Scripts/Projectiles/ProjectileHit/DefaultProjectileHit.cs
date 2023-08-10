
using UnityEngine;

public class DefaultProjectileHit : IProjectileHit
{
    public void Hit(Collider collider, Projectile projectile)
    {
        if (collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(projectile.TypeDamage, projectile.Damage);
        }
        projectile.ProjectilePool.Release(projectile);
    }
}
