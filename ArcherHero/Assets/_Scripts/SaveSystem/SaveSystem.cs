using UnityEngine;
using System.IO;

namespace SaveSystem
{
public static class SaveSystem
{
    private static string savePathSkills;
    private static string savePathStats;
    private const string characterSkillsFileName = "characterSkills.json";
    private const string characterStatsFileName = "characterStats.json";

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {   
      
        savePathSkills = Path.Combine(Application.persistentDataPath, characterSkillsFileName);
        savePathStats = Path.Combine(Application.persistentDataPath, characterStatsFileName);
        
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
        string json = JsonUtility.ToJson(characterSkills);
        File.WriteAllText(savePathSkills, json);
    }

    public static void LoadCharacterSkills(CharacterSkills characterSkills)
    {
        if (File.Exists(savePathSkills))
        {
            string json = File.ReadAllText(savePathSkills);
            JsonUtility.FromJsonOverwrite(json, characterSkills);
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
    }
}
}