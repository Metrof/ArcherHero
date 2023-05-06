
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterSkills" ,menuName = "ArcherHero/CharacterSkills")]

public class CharacterSkills : ScriptableObject
{
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int _currentXP = 0;
    [SerializeField] private int _xpToLevelUp = 100;
    [SerializeField] private int _skillPoints = 0;
    
    public int CurrentXP => _currentXP;
    public int CurrentLevel => _currentLevel;
    public int XPToLevelUp => _xpToLevelUp;
    public int SkillPoints => _skillPoints;

   /* public int Experience
    {
        get => _currentXP;
        set => _currentXP = value;
    }*/
    
    public void GainExperience(int amount)
    {
        _currentXP += amount;

        while (_currentXP >= _xpToLevelUp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _currentLevel++;
        _currentXP -= _xpToLevelUp;
        _xpToLevelUp = (int)(_xpToLevelUp * 1.5f);
        _skillPoints++;
    }

    public void SetValues(int currentLevel, int currentXP, int xpToLevelUp, int skillPoints)
    {
        _currentLevel = currentLevel;
        _currentXP = currentXP;
        _xpToLevelUp = xpToLevelUp;
        _skillPoints = skillPoints;   
    }
}

    
    


