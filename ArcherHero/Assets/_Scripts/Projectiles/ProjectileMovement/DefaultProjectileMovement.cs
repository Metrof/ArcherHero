using UnityEngine;

public class DefaultProjectileMovement : IProjectileMovement
{
    public void Move(Projectile projectile, Transform target, float speed)
    {
        projectile.transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
