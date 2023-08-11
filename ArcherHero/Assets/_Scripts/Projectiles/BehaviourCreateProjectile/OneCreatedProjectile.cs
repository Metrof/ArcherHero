using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class OneCreatedProjectile : IBehaviorCreateProjectile
{

    public List<Projectile> Create(Vector3 pointSpawnProjectile, Transform target, ObjectPool<Projectile> pool)
    {
        List<Projectile> _projectiles = new();

        Projectile newProjectile = pool.Get();
        _projectiles.Add(newProjectile);
        newProjectile.transform.position = pointSpawnProjectile;
        SetRotation(target, newProjectile);

        return _projectiles;
    }
    private void SetRotation(Transform target, Projectile newProjectile)
    {
        Vector3 targetPosition = target.position;
        targetPosition.y = newProjectile.transform.position.y;
        newProjectile.transform.LookAt(targetPosition);
    }
}
