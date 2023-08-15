using UnityEngine;

public class RicochetProjectileHit : IProjectileHit
{
    private const float _distance = 50;
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
                    projectile.Count++;

                    Vector3 direction = Vector3.Reflect(projectile.transform.forward, hit.normal);
                    projectile.NewMoveSequence();
                    
                    projectile.transform.rotation = Quaternion.LookRotation(direction);

                    projectile.ProjectileMovement.Move(projectile, projectile.transform.forward * _distance, projectile.MoveSpeedProjectile);
                }
            }
            else
            {
                projectile.ProjectilePool.Release(projectile);
            }
        }
    }
}
