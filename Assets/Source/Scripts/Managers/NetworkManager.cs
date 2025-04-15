using Newtonsoft.Json;
using System;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class NetworkManager : MonoBehaviour, INetworkManager
    {
        #region GET
        public void GetCustomLevelList(Action<LevelListNetworkData> callback, Action error) =>
            APIServer.GET(RequestAddresses.GET_CUSTOM_LEVEL_LIST, 
                (s) => { ParseData(s, callback); }, 
                (s) => { error?.Invoke();}
             );

        public void GetFullCustomLevelData(string id, Action<LevlelNetworkData> callback, Action error) =>
            APIServer.GET(RequestAddresses.GET_FULL_CUSTOM_LEVEL_DATA,
                (s) => { ParseData(s, callback); },
                (s) => { error?.Invoke(); }
             );

        public void GetPlayerData(string id, Action<PlayerNetworkData> callback, Action error)=>
             APIServer.GET(string.Format(RequestAddresses.GET_PLAYER_DATA, id),
                (s) => { ParseData(s, callback); },
                (s) => { error?.Invoke(); }
             );
        #endregion

        #region POST
        public void PostCreateNewLevel(CreateLevelNetworkData ctx, Action<StatusNetworkData> callback, Action error)=>
            APIServer.POST(SerializeData(ctx),
                RequestAddresses.GET_FULL_CUSTOM_LEVEL_DATA,
                (s) => { ParseData(s, callback); },
                (s) => { error?.Invoke(); }
             );

        public void PostCreateNewPlayer(LoginNetworkData ctx, Action<PlayerNetworkData> callback, Action error) =>
            APIServer.POST(SerializeData(ctx),
                RequestAddresses.POST_PLAYERS_CREATE,
                (s) => { ParseData(s, callback); },
                (s) => { error?.Invoke(); }
             );

        public void PostLoginPlayer(LoginNetworkData ctx, Action<PlayerNetworkData> callback, Action error)=>
            APIServer.POST(SerializeData(ctx),
                RequestAddresses.POST_PLAYERS_LOGIN,
                (s) => { ParseData(s, callback); },
                (s) => { error?.Invoke(); }
             );

        #endregion

        private void ParseData<T>(string data, Action<T> callback)
        {
            if (string.IsNullOrEmpty(data) || data == "{}")
            {
                callback?.Invoke(default);
                return;
            }
            Debug.Log(data);
            T result = JsonConvert.DeserializeObject<T>(data);

            callback?.Invoke(result);
        }

        private string SerializeData<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
