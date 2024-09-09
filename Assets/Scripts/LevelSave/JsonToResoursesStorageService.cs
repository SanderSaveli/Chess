using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class JsonToResourcesStorageService : IStorageService
    {
        public void Save(string key, object data, Action<bool> callback = null)
        {
            string path = BuildPersistentPath(key);
            string jsonFile = JsonConvert.SerializeObject(data, Formatting.Indented);

            try
            {
                using (var fileStream = new StreamWriter(path))
                {
                    fileStream.Write(jsonFile);
                }

                Debug.Log($"Файл сохранён в {path}");
                callback?.Invoke(true);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при сохранении файла: {ex.Message}");
                callback?.Invoke(false);
            }
        }
        public void Load<T>(string key, Action<T> callback)
        {
            try
            {
                TextAsset jsonFile = Resources.Load<TextAsset>(key);
                if (jsonFile == null)
                {
                    Debug.LogError($"Файл {key} не найден в Resources.");
                    callback.Invoke(default(T));
                    return;
                }

                T data = JsonConvert.DeserializeObject<T>(jsonFile.text);
                callback.Invoke(data);

                Debug.Log($"Файл {key} загружен из Resources.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при загрузке из Resources: {ex.Message}");
                callback?.Invoke(default(T));
            }
        }

        private string BuildPersistentPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key + ".json");
        }
    }
}
