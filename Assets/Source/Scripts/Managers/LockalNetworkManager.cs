using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class LockalNetworkManager : MonoBehaviour, INetworkManager
    {
        private IStorageService _storageService;

        [Inject] 
        public void Construct(IStorageService storageService)
        {
            _storageService = storageService;
        }


        public void GetFullCustomLevelData(string id, Action<LevlelNetworkData> callback, Action error)
        {
            _storageService.Load<LevelData>(Const.CUSTOM_LEVLES_KEY + id, data =>
            {
                callback?.Invoke(new LevlelNetworkData(id, data));
            });
        }

        public void GetCustomLevelList(Action<LevelListNetworkData> callback, Action error)
        {
            LevelListNetworkData levelList = new LevelListNetworkData();
            levelList.level_list = GetLevelsBriefDatas();
            callback?.Invoke(levelList);
        }

        public void PostCreateNewLevel(CreateLevelNetworkData ctx, Action<StatusNetworkData> callback, Action<string> error)
        {
            string key = Const.CUSTOM_LEVLES_KEY + ctx.level_name;
            _storageService.Save(key, ctx.level, 
                (bool b)=>CallStatus(b, callback, error));
        }

        private List<BriefLevelNetworkData> GetLevelsBriefDatas()
        {
            List<string> levelNames = GetAllCustomLevels();
            List<BriefLevelNetworkData> briefLevelDatas = new();
            int i = 1;
            foreach (string levelName in levelNames)
            {
                BriefLevelNetworkData briefData = new BriefLevelNetworkData(levelName, "Lockal", "lockal_level_" + i);

                briefLevelDatas.Add(briefData);
                i++;
            }

            return briefLevelDatas;
        }

        private List<string> GetAllCustomLevels()
        {
            List<string> levels = new List<string>();

            string path = BuildStreamingAssetsPath("CustomLevels/");

            if (!Directory.Exists(path))
            {
                throw new Exception($"Директория {path} не найдена.");
            }

            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                string fileExtension = Path.GetExtension(file);
                if (fileExtension != ".meta" && (File.GetAttributes(file) & FileAttributes.Hidden) == 0)
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                    levels.Add(fileNameWithoutExtension);
                }
            }
            return levels;
        }
        private string BuildStreamingAssetsPath(string key)
        {
            return Path.Combine(Application.streamingAssetsPath, key);
        }

        private void CallStatus(bool isSucsess, Action<StatusNetworkData> callback, Action<string> error)
        {
            if (isSucsess)
            {
                callback?.Invoke(new StatusNetworkData());
            }
            else
            {
                error?.Invoke("");
            }
        }

        public void PostCreateNewPlayer(LoginNetworkData ctx, Action<PlayerNetworkData> callback, Action<string> error)
        {
            throw new NotImplementedException();
        }

        public void GetPlayerData(string id, Action<PlayerNetworkData> callback, Action error)
        {
            throw new NotImplementedException();
        }

        public void PostLoginPlayer(LoginNetworkData ctx, Action<PlayerNetworkData> callback, Action error)
        {
            throw new NotImplementedException();
        }

        public void PostLoginPlayer(LoginNetworkData ctx, Action<PlayerNetworkData> callback, Action<string> error)
        {
            throw new NotImplementedException();
        }
    }
}
