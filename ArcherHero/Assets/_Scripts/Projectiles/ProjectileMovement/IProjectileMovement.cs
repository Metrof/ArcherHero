using UnityEngine;

public interface IProjectileMovement
{
    void Move(Projectile projectile, Transform target, float speed);
}
