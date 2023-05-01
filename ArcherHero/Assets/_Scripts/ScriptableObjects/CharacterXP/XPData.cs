using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class XPData : MonoBehaviour
{
    public int _currentLevel;
    public int _currentXP;
    public int _xpToLevelUp;
    public int _skillPoints;

    public XPData (CharacterSkills characterSkills)
    {
        _currentLevel = characterSkills.CurrentLevel;
        _currentXP = characterSkills.CurrentXP;
        _xpToLevelUp = characterSkills.XPToLevelUp;
        _skillPoints = characterSkills.SkillPoints;
    }
}
