using UnityEngine;

public interface IProjectileMovement
{
    void Move(Transform projectile, Vector3 direction, float speed);
}
