using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ThreeCreatedProjectile : IBehaviorCreateProjectile
{
    private readonly List<float> _anglesRotation = new() 
    {
        0,
        15,
        -15,
    };
    public List<Projectile> Create(Transform pointSpawnProjectile, Transform target, ObjectPool<Projectile> pool)
    {
        List<Projectile> _projectiles = new();

        for (int i = 0; i < _anglesRotation.Count; i++)
        {
            Projectile newProjectile = pool.Get();
            _projectiles.Add(newProjectile);
            newProjectile.transform.position = pointSpawnProjectile.position;
            SetRotation(target, newProjectile, _anglesRotation[i]);
        }

        return _projectiles;
    }
    private void SetRotation(Transform target, Projectile newProjectile, float angle)
    {
        Vector3 targetPosition = target.position;
        targetPosition.y = newProjectile.transform.position.y;
        newProjectile.transform.LookAt(targetPosition);
        newProjectile.transform.Rotate(Vector3.up, angle);
    }
}