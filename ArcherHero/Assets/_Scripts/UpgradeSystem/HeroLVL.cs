using System;

public class HeroLVL
{
    public event Action<int> OnIncreasedLevelEvent;
    public event Action<int> OnExperienceAddedEvent;

    public int CurrentLvl { get; private set; }
    public int ToLevelUP { get => CurrentLvl * _lvlBoostFactor; }

    private readonly int _lvlBoostFactor;
    private readonly int _maxLvl;
    private int _experience;

    public HeroLVL(int currentLVL, int lvlBoostFactor, int maxLvl)
    {
        CurrentLvl = currentLVL;
        _maxLvl = maxLvl;
        _lvlBoostFactor = lvlBoostFactor;
    }

    public void AddExperience(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }
        if(CurrentLvl >=  _maxLvl)
        {
            return;
        }
        _experience += amount;
        CheckLevelUp();
        OnExperienceAddedEvent?.Invoke(_experience);
    }

    private void CheckLevelUp()
    {
        if (_experience >= ToLevelUP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _experience -= ToLevelUP;
        CurrentLvl++;
        OnIncreasedLevelEvent?.Invoke(CurrentLvl);
    }
}
