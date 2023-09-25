using System;
using System.Threading;

public class ChangeProjectileType
{
    private CancellationTokenSource _cTS;
    private readonly ProjectileOwner _owner;
    private readonly Weapon _weapon;
    private readonly ProjectilePool _pool;
    private readonly TypeDamage _currentTypeDamage;
    private readonly TypeDamage _newTypeDamage;

    public ChangeProjectileType(ProjectileOwner owner, Weapon weapon, ProjectilePool pool, TypeDamage currentTypeDamage, TypeDamage newTypeDamage, float timeSeconds = 0)
    {
        _cTS = new CancellationTokenSource();
        _owner = owner;
        _weapon = weapon;
        _pool = pool;
        _currentTypeDamage = currentTypeDamage;
        _newTypeDamage = newTypeDamage;

        AsyncDelay.Delay(TimeSpan.FromSeconds(timeSeconds), StartChange, Stop, token: _cTS.Token).Forget();
    }

    private void StartChange()
    {
        _weapon.ChangeProjectiles(_pool.GetPool(_owner, _newTypeDamage));
    }

    public void Stop()
    {
        _weapon.ChangeProjectiles(_pool.GetPool(_owner, _currentTypeDamage));
        _cTS.Cancel();
    }
}

