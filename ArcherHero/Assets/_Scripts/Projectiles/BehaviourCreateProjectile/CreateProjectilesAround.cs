using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public abstract class CreateProjectilesAround : IBehaviorCreateProjectile
{
    protected abstract List<float> AnglesRotation { get; }
    public List<Projectile> Create(Vector3 pointSpawnProjectile, Transform target, ObjectPool<Projectile> pool)
    {
        List<Projectile> _projectiles = new();

        for (int i = 0; i < AnglesRotation.Count; i++)
        {
            Projectile newProjectile = pool.Get();
            _projectiles.Add(newProjectile);
            newProjectile.transform.position = pointSpawnProjectile;
            SetRotation(target, newProjectile, AnglesRotation[i]);
            newProjectile.transform.Translate(Vector3.forward);
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