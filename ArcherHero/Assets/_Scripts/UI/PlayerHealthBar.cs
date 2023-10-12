using PlayerStats;
using Zenject;

public class PlayerHealthBar : HealthBar
{
    private CharacterStats _characterStats;

    [Inject]
    private void Construct(CharacterStats characterStats)
    {
        _characterStats = characterStats;
    }

    public override void Start()
    {   
        base.Start();
        _characterStats.MaxHP.OnChangeUpgradeLvlEvent += ChangeMaxHP;
    }

    private void ChangeMaxHP(StatInfo obj)
    {
        _currentFillAmount = obj.CurrentValue;
    }
}
