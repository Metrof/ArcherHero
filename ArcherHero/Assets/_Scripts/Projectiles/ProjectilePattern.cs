using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePattern
{
    public ObjectPool<Projectile> ProjectilePool;

    private IProjectileMovement _currentMovement;
    private IProjectileHit _currentHit;
    private IBehaviorCreateProjectile _currentCreationBehavior;

    private readonly Dictionary<ProjectileMovementType, IProjectileMovement> _movementDict = new()
    {
        [ProjectileMovementType.Default] = new DefaultProjectileMovement(),
        [ProjectileMovementType.Hinged] = new MovementHingedTrajectory(),
    };

    private readonly Dictionary<ProjectileHitType, IProjectileHit> _hitDict = new()
    {
        [ProjectileHitType.Default] = new DefaultProjectileHit(),
        [ProjectileHitType.Ricochet] = new RicochetProjectileHit(),
        [ProjectileHitType.EightAround] = new EightAroundProjectileHit(),
    };

    private readonly Dictionary<ProjectileCreationType, IBehaviorCreateProjectile> _creationDict = new()
    {
        [ProjectileCreationType.One] = new OneCreatedProjectile(),
        [ProjectileCreationType.Three] = new ThreeCreatedProjectile(),
        [ProjectileCreationType.Double] = new DoubleCreatedProjectile(),
        [ProjectileCreationType.Triple] = new TripleCreatedProjectile(),
        [ProjectileCreationType.EightAround] = new EightCreatedProjectilesAround(),
    };

    public ProjectilePattern(ObjectPool<Projectile> objectPool)
    {
        ProjectilePool = objectPool;

        SetMovement(ProjectileMovementType.Default)
            .SetHit(ProjectileHitType.Default)
            .SetCreation(ProjectileCreationType.One);
    }

    public void Create(Transform pointSpawnProjectile, Transform target, int damage)
    {
        var projectiles = _currentCreationBehavior.Create(pointSpawnProjectile.position, target, ProjectilePool);

        foreach (var projectile in projectiles)
        {
            projectile.Initialize(damage, target, _currentMovement, _currentHit);
            projectile.Move();
        }
    }


    public ProjectilePattern SetMovement(ProjectileMovementType moveType)
    {
        _currentMovement = _movementDict[moveType];

        return this;
    }

    public ProjectilePattern SetHit(ProjectileHitType hitType)
    {
        _currentHit = _hitDict[hitType];

        return this;
    }

    public ProjectilePattern SetCreation(ProjectileCreationType amountType)
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
    EightAround,
}

public enum ProjectileMovementType
{
    Default,
    Hinged,
}

public enum ProjectileHitType
{
    Default,
    Ricochet,
    EightAround,
}
