
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

    public void GainExperience(int amount)
    {
        _currentXP += amount;

        if(_currentXP >= _xpToLevelUp)
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

}

    
    


