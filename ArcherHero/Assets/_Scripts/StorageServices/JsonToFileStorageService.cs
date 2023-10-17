using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonToFileStorageService : IStorageService
{
    public void Save(string key, object data, Action<bool> callback = null)
    {
        string path = BuildPath(key);
        string json = JsonConvert.SerializeObject(data);

        if (!File.Exists(path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (var fileStream = File.Create(path))
            {
                fileStream.Close();
            }
        }

        using (var fileStream = new StreamWriter(path))
        {
            fileStream.Write(json);
        }

        callback?.Invoke(true);
    }

    public void Load<T>(string key, Action<T> callback)
    {
        string path = BuildPath(key);
        if (File.Exists(path))
        {
            using (var fileStream = new StreamReader(path))
            {
                var json = fileStream.ReadToEnd();
                var data = JsonConvert.DeserializeObject<T>(json);

                callback.Invoke(data);
            }
        }
        else
        {
            callback.Invoke(default);
        }
    }

    private string BuildPath(string key)
    {
        return Path.Combine(Application.persistentDataPath, key);
    }
}

