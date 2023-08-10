using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileFactory
{
    public ObjectPool<Projectile> ProjectilePool;

    private IProjectileMovement _currentMovement;
    private IProjectileHit _currentHit;
    private ICreateProjectileBehavior _currentCreationBehavior;

    private readonly Dictionary<ProjectileMovementType, IProjectileMovement> _movementDict = new()
    {
        [ProjectileMovementType.Default] = new DefaultProjectileMovement(),
    };

    private readonly Dictionary<ProjectileHitType, IProjectileHit> _hitDict = new()
    {
        [ProjectileHitType.Default] = new DefaultProjectileHit(),
    };

    private readonly Dictionary<ProjectileCreationType, ICreateProjectileBehavior> _creationDict = new()
    {
        [ProjectileCreationType.One] = new OneCreatingProjectile(),
    };

    public ProjectileFactory(ObjectPool<Projectile> objectPool)
    {
        ProjectilePool = objectPool;

        SetMovement(ProjectileMovementType.Default)
            .SetHit(ProjectileHitType.Default)
            .SetAmount(ProjectileCreationType.One);
    }

    public void Create(Transform pointSpawnProjectile, Transform target, int damage)
    {
        var projectiles = _currentCreationBehavior.Create(pointSpawnProjectile, target, ProjectilePool);

        foreach (var projectile in projectiles)
        {
            projectile.Initialize(damage, target.position, _currentMovement, _currentHit);
        }
    }


    public ProjectileFactory SetMovement(ProjectileMovementType moveType)
    {
        _currentMovement = _movementDict[moveType];

        return this;
    }

    public ProjectileFactory SetHit(ProjectileHitType hitType)
    {
        _currentHit = _hitDict[hitType];

        return this;
    }

    public ProjectileFactory SetAmount(ProjectileCreationType amountType)
    {
        _currentCreationBehavior = _creationDict[amountType];

        return this;
    }
}

public enum ProjectileCreationType
{
    One,
    Three,
    Double,
    Triple,
}

public enum ProjectileMovementType
{
    Default,
}

public enum ProjectileHitType
{
    Default,
}
