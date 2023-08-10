using UnityEngine;

public class DefaultProjectileMovement : IProjectileMovement
{
    public void Move(Transform projectile, Vector3 direction, float speed)
    {
        projectile.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
