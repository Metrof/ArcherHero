using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace SaveSystem
{

[System.Serializable]
public static class SaveSystem
{
    private static string savePathSkills;
    private static string savePathStats;
    private static string savePathPerk;
    private const string characterSkillsFileName = "characterSkills.json";
    private const string characterStatsFileName = "characterStats.json";
    private const string characterPerkData = "characterPerk.json";
    

    public static void Initialize()
    {   
        savePathSkills = Path.Combine(Application.persistentDataPath, characterSkillsFileName);
        savePathStats = Path.Combine(Application.persistentDataPath, characterStatsFileName);
        savePathPerk = Path.Combine(Application.persistentDataPath, characterPerkData);
        
       /*
        if (Application.platform == RuntimePlatform.Android)
        {
            savePathSkills = Path.Combine(Application.persistentDataPath, characterSkillsFileName);
            savePathStats = Path.Combine(Application.persistentDataPath, characterStatsFileName);
        }
        else
        {
            savePathSkills = Path.Combine(Application.dataPath, characterSkillsFileName);
            savePathStats = Path.Combine(Application.dataPath, characterStatsFileName);
        }*/
    }

    public static void SaveCharacterSkills(CharacterSkills characterSkills)
    {
        if (characterSkills != null)
        {
            string json = JsonUtility.ToJson(characterSkills);
            File.WriteAllText(savePathSkills, json);
        }
        else
        {
            Debug.LogError("CharacterSkills is null. Unable to save.");
        }
    }

        public static void LoadCharacterSkills(CharacterSkills characterSkills)
    {
        if (File.Exists(savePathSkills))
        {
            string json = File.ReadAllText(savePathSkills);
            JsonUtility.FromJsonOverwrite(json, characterSkills);
        }
        else
        {
            Debug.LogWarning("CharacterSkills save file does not exist. Unable to load.");
        }
    }

    public static void SaveCharacterStats(CharacterStats characterStats)
    {
        string json = JsonUtility.ToJson(characterStats);
        if (json != null)
        {
            File.WriteAllText(savePathStats, json);
        }
    }

    public static void LoadCharacterStats(CharacterStats characterStats)
    {
        if (File.Exists(savePathStats))
        {
            string json = File.ReadAllText(savePathStats);
            JsonUtility.FromJsonOverwrite(json, characterStats);
        }
        else
        {
            Debug.LogError("CharacterStats save file does not exist. Unable to load.");
        }
    }


   public static void SavePerkData(Dictionary<PerkManager.PerkType, PerkManager.PerkStatus> perkData)
   {

        if(perkData != null)
        {
            string json = JsonConvert.SerializeObject(perkData);
            File.WriteAllText(savePathPerk, json);
        }
        else
        {
            Debug.LogError("PerkData is null. Unable to save.");
        }
   }

    public static Dictionary<PerkManager.PerkType, PerkManager.PerkStatus> LoadPerkData()
    {
            try
            {
                string json = File.ReadAllText(savePathPerk);
                return JsonConvert.DeserializeObject<Dictionary<PerkManager.PerkType, PerkManager.PerkStatus>>(json);
            }
            catch (Exception)
            {
                return GetDefaultPerkData();
            }
    }

    private static Dictionary<PerkManager.PerkType, PerkManager.PerkStatus> GetDefaultPerkData()
    {
        Dictionary<PerkManager.PerkType, PerkManager.PerkStatus> perkData = new Dictionary<PerkManager.PerkType, PerkManager.PerkStatus>();
        Debug.Log("GetDefaultPerkData()");
        foreach (PerkManager.PerkType perkType in Enum.GetValues(typeof(PerkManager.PerkType)))
        {
            perkData.Add(perkType, PerkManager.PerkStatus.NotAvailable);
        }

        perkData[PerkManager.PerkType.Perk1] = PerkManager.PerkStatus.Available;
        perkData[PerkManager.PerkType.Perk8] = PerkManager.PerkStatus.Available;

        return perkData;
    }
}
}