using UnityEngine;

public interface IProjectileHit
{
    void Hit(Collider collider, Projectile projectile);
}