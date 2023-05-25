using UnityEngine;


namespace SaveSystem
{
public class SaveManager : MonoBehaviour
{
    [SerializeField] private CharacterSkills _characterSkills;
    [SerializeField] private CharacterStats _characterStats;

    private void Awake()
    {
        _characterSkills.GainExperience(DataHolder.PlayerBounty.MinedExp);
        _characterStats.Money += DataHolder.PlayerBounty.MinedGold;
        DataHolder.PlayerBounty.ClearStruct();
        Save();
        Load();
    }


    //private void  OnApplicationFocus(bool hasFocus)
    //{  
    //    if (!hasFocus)
    //    {
    //        Save();
    //    }
    //}

    private void OnApplicationQuit()
    {
        Save();
    }
   
    
    public void Save()
    {   
        SaveSystem.SaveCharacterStats(_characterStats);
        SaveSystem.SaveCharacterSkills(_characterSkills);
        Debug.Log("Data saved");
    }
 
   public void Load()
    {   
        SaveSystem.LoadCharacterStats(_characterStats);
        SaveSystem.LoadCharacterSkills(_characterSkills);
        Debug.Log("Data loaded");
    }
}
}
