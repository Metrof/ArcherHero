using UnityEngine;


namespace SaveSystem
{
[System.Serializable]
public class SaveManager : MonoBehaviour
{
    [SerializeField] private CharacterSkills _characterSkills;
    [SerializeField] private CharacterStats _characterStats;
    PerkManager _perkManager;

    private void Awake()
    {   
        SaveSystem.Initialize();
        _perkManager = FindObjectOfType<PerkManager>();
        Load();
    }
   

    private void  OnApplicationFocus(bool hasFocus)
    {  
        if (!hasFocus)
        {
            Save();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
   
    
    public void Save()
    {   
        SaveSystem.SaveCharacterStats(_characterStats);
        SaveSystem.SaveCharacterSkills(_characterSkills);
        _perkManager.SavePerkData(); 
    }
 
   public void Load()
    {   
        SaveSystem.LoadCharacterStats(_characterStats);
        SaveSystem.LoadCharacterSkills(_characterSkills);
    }
}
}
