using System;
using System.Threading;

public class ChangeProjectilePattern
{
    private CancellationTokenSource _cTS;
    private readonly Weapon _weapon;
    private readonly ProjectileCreationType _creationType;
    private readonly ProjectileMovementType _movementType;
    private readonly ProjectileHitType _hitType;

    public ChangeProjectilePattern(Weapon weapon, ProjectileCreationType creation, ProjectileMovementType movement, ProjectileHitType hit, float durationSeconds) 
    {
        _cTS = new CancellationTokenSource();
        _weapon = weapon;
        _creationType = creation;
        _movementType = movement;
        _hitType = hit;

        AsyncDelay.Delay(TimeSpan.FromSeconds(durationSeconds), StartChange, Stop, token: _cTS.Token).Forget();
    }

    private void StartChange()
    {
        _weapon.ProjectilePattern
            .SetCreation(_creationType)
            .SetMovement(_movementType)
            .SetHit(_hitType);
    }

    public void Stop()
    {
        _weapon.ProjectilePattern
            .SetCreation(ProjectileCreationType.One)
            .SetMovement(ProjectileMovementType.Default)
            .SetHit(ProjectileHitType.Default);

        _cTS.Cancel();
    }
}

