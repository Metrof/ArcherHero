using UnityEngine;

public class ShootThroughProjectileHit : IProjectileHit
{
    public void Hit(Collider collider, Projectile projectile)
    {
        if(collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(projectile.TypeDamage, projectile.Damage);
        }
        else
        {
            projectile.ProjectilePool.Release(projectile);
        }
    }
}
