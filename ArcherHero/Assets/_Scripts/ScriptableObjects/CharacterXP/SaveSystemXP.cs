
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private CharacterSkills _characterSkills;

    private const string LevelKey = "CurrentLevel";
    private const string XPKey = "CurrentXP";
    private const string XPToLevelUpKey = "XPToLevelUp";
    private const string SkillPointsKey = "SkillPoints";
    

    /* private void  OnApplicationFocus(bool hasFocus)
    {  
        // ???????
    }*/

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Awake()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(LevelKey, _characterSkills.CurrentLevel);
        PlayerPrefs.SetInt(XPKey, _characterSkills.CurrentXP);
        PlayerPrefs.SetInt(XPToLevelUpKey, _characterSkills.XPToLevelUp);
        PlayerPrefs.SetInt(SkillPointsKey, _characterSkills.SkillPoints);
        PlayerPrefs.Save();
        
        Debug.Log("Data saved");
    }

   public void Load()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", _characterSkills.CurrentLevel);
        int currentXP = PlayerPrefs.GetInt("CurrentXP", _characterSkills.CurrentXP);
        int xpToLevelUp = PlayerPrefs.GetInt("XPToLevelUp", _characterSkills.XPToLevelUp);
        int skillPoints = PlayerPrefs.GetInt("SkillPoints", _characterSkills.SkillPoints);

        _characterSkills.SetValues(currentLevel, currentXP, xpToLevelUp, skillPoints);

        Debug.Log("Data loaded");
    }
}
