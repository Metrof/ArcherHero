using PlayerStats;
using System;
using Zenject;

public class PlayerHealthBar : HealthBar
{   
    public override void Start()
    {   
        if(_entity is Player player)
        {
            player.OnPlayerRestartedEvent += GetChangeMaxHP;
        }

    }

    private void GetChangeMaxHP()
    {
        _currentFillAmount = _entity.currentHealth;
        ChangeHealth((int)_currentFillAmount);
    }
}
