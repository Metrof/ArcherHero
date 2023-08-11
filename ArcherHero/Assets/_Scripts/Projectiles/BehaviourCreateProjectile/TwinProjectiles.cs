using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public abstract class TwinProjectiles : IBehaviorCreateProjectile
{
    protected abstract List<float> ShiftX {  get; }

    public List<Projectile> Create(Vector3 pointSpawnProjectile, Transform target, ObjectPool<Projectile> pool)
    {
        List<Projectile> _projectiles = new();

        for (int i = 0; i < ShiftX.Count; i++)
        {
            Projectile newProjectile = pool.Get();
            _projectiles.Add(newProjectile);
            newProjectile.transform.position = pointSpawnProjectile;
            SetRotation(target, newProjectile);
            newProjectile.transform.Translate(ShiftX[i], 0, 0);
        }

        return _projectiles;
    }

    private void SetRotation(Transform target, Projectile newProjectile)
    {
        Vector3 targetPosition = target.position;
        targetPosition.y = newProjectile.transform.position.y;
        newProjectile.transform.LookAt(targetPosition);
    }
}