using UnityEngine;

public interface IProjectileMovement
{
    void Move(Projectile projectile, Vector3 target, float speed);
}
