using UnityEngine;

public class RicochetProjectileHit : IProjectileHit
{
    private const int _maxRicochetCount = 3;
    public void Hit(Collider collider, Projectile projectile)
    {
        if (collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(projectile.TypeDamage, projectile.Damage);
            projectile.ProjectilePool.Release(projectile);
        }
        else
        {
            if (projectile.Count < _maxRicochetCount)
            {
                RaycastHit hit;
                if (Physics.Raycast(projectile.transform.position, projectile.transform.forward, out hit))
                {
                    Vector3 direction = Vector3.Reflect(projectile.transform.forward, hit.normal);
                    projectile.transform.rotation = Quaternion.LookRotation(direction);
                }
            }
            else
            {
                projectile.ProjectilePool.Release(projectile);
            }
        }
    }
}
