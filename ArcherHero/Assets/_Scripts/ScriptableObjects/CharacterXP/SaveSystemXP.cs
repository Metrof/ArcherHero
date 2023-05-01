
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystemXP
{
    public static void SaveXP(CharacterSkills characterSkills)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/character.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        XPData data = new XPData(characterSkills);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static XPData LoadXP()
    {
        string path = Application.persistentDataPath + "/character.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            XPData data = formatter.Deserialize(stream) as XPData;

            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
