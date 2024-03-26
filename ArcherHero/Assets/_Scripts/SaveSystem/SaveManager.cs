using UnityEngine;
using Zenject;


namespace SaveSystem
{
[System.Serializable]
public class SaveManager : MonoBehaviour
{
    [SerializeField] private CharacterSkills _characterSkills;
    [SerializeField] private CharacterStats _characterStats;
    PerkManager _perkManager;

    private DataHolderTestZ _holderTestZ;

    [Inject]
    public void Construct(DataHolderTestZ holderTestZ)
    {
        _holderTestZ = holderTestZ;
    }
    private void Awake()
    {
        SaveSystem.Initialize();
        
        _perkManager = FindObjectOfType<PerkManager>();


        _characterSkills.GainExperience(_holderTestZ.PlayerBounty.MinedExp);
        _characterStats.Money += _holderTestZ.PlayerBounty.MinedGold;
        _holderTestZ.PlayerBounty.ClearStruct();
       

        //Save();

        //Load();
    }
    private void Start()
    {
            Save();

            Load();
    }


    private void OnApplicationFocus(bool hasFocus)
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
        Debug.Log("Save") ;
        SaveSystem.SaveCharacterStats(_characterStats);
        SaveSystem.SaveCharacterSkills(_characterSkills);
        _perkManager.SavePerkData(); 
    }
 
   public void Load()
    {
        Debug.Log("Load");
        SaveSystem.LoadCharacterStats(_characterStats);
        SaveSystem.LoadCharacterSkills(_characterSkills);
    }
}
}
