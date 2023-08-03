using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProjectileMovement : IProjectileMovement
{
    public void Move(Transform projectile, Vector3 direction, float speed)
    {
        projectile.position += direction * Time.deltaTime * speed;
    }
}
