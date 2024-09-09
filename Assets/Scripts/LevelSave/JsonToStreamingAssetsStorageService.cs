using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class JsonToStreamingAssetsStorageService : IStorageService
    {
        public void Save(string key, object data, Action<bool> callback = null)
        {
            string tempPath = BuildTempPath(key);
            string jsonFile = JsonConvert.SerializeObject(data, Formatting.Indented);

            try
            {
                using (var fileStream = new StreamWriter(tempPath))
                {
                    fileStream.Write(jsonFile);
                }

                string streamingAssetsPath = BuildStreamingAssetsPath(key);
                File.Copy(tempPath, streamingAssetsPath, true);
                //File.Delete(tempPath); 

                Debug.Log($"���� ������� � {streamingAssetsPath}");
                callback?.Invoke(true);
            }
            catch (Exception ex)
            {
                Debug.LogError($"������ ��� ���������� �����: {ex.Message}");
                callback?.Invoke(false);
            }
        }

        public void Load<T>(string key, Action<T> callback)
        {
            string path = BuildStreamingAssetsPath(key);

            if (!File.Exists(path))
            {
                Debug.LogError($"���� {key} �� ������ � " + BuildStreamingAssetsPath(key));
                callback.Invoke(default(T));
                return;
            }

            try
            {
                using (var fileStream = new StreamReader(path))
                {
                    string jsonFile = fileStream.ReadToEnd();
                    T data = JsonConvert.DeserializeObject<T>(jsonFile);
                    callback.Invoke(data);
                }

                Debug.Log($"���� {key} �������� �� {path}.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"������ ��� �������� �� StreamingAssets: {ex.Message}");
                callback?.Invoke(default(T));
            }
        }

        private string BuildTempPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key + ".json");
        }

        private string BuildStreamingAssetsPath(string key)
        {
            return Path.Combine(Application.streamingAssetsPath, key + ".json");
        }
    }
}
