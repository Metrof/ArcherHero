using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class OneCreatingProjectile : ICreateProjectileBehavior
{

    public List<Projectile> Create(Transform pointSpawnProjectile, Transform target, ObjectPool<Projectile> pool)
    {
        List<Projectile> _projectiles = new();

        Projectile newProjectile = pool.Get();
        _projectiles.Add(newProjectile);
        newProjectile.transform.position = pointSpawnProjectile.position;
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
