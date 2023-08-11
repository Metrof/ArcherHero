using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public abstract class CreateProjectilesAround
{
    protected abstract List<float> AnglesRotation { get; }
    public List<Projectile> Create(Transform pointSpawnProjectile, Transform target, ObjectPool<Projectile> pool)
    {
        List<Projectile> _projectiles = new();

        for (int i = 0; i < AnglesRotation.Count; i++)
        {
            Projectile newProjectile = pool.Get();
            _projectiles.Add(newProjectile);
            newProjectile.transform.position = pointSpawnProjectile.position;
            SetRotation(target, newProjectile, AnglesRotation[i]);
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