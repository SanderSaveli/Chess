using Newtonsoft.Json;
using System;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class NetworkManager : MonoBehaviour, INetworkManager
    {
        #region GET
        public void GetCustomLevelList(LevelListContext ctx, Action<LevelListNetworkData> callback, Action error) =>
            StartCoroutine(APIServer.GET(string.Format(RequestAddresses.GET_CUSTOM_LEVEL_LIST, ctx.PerPage, ctx.Page), 
                (s) => { ParseData(s, callback); }, 
                (s) => { error?.Invoke();}
             ));

        public void GetFullCustomLevelData(int id, Action<LevlelNetworkData> callback, Action error) =>
            StartCoroutine(APIServer.GET(string.Format(RequestAddresses.GET_FULL_CUSTOM_LEVEL_DATA, id),
                (s) => { ParseData(s, callback); },
                (s) => { error?.Invoke(); }
             ));

        public void GetPlayerData(string id, Action<PlayerNetworkData> callback, Action error)=>
             StartCoroutine(APIServer.GET(string.Format(RequestAddresses.GET_PLAYER_DATA, id),
                (s) => { ParseData(s, callback); },
                (s) => { error?.Invoke(); }
             ));
        #endregion

        #region POST
        public void PostCreateNewLevel(CreateLevelNetworkData ctx, Action<StatusNetworkData> callback, Action<string> error)=>
            StartCoroutine(APIServer.POST(SerializeData(ctx),
                RequestAddresses.POST_CREATE_NEW_LEVEL,
                (s) => { ParseData(s, callback); },
                (s) => { ParseData(s, callback); }
             ));

        public void PostCreateNewPlayer(LoginNetworkData ctx, Action<PlayerNetworkData> callback, Action<string> error)=>
            StartCoroutine(APIServer.POST(SerializeData(ctx),
                RequestAddresses.POST_PLAYERS_CREATE,
                (s) => { ParseData(s, callback); },
                (s) => { TryParseError(s, error); }
             ));

        public void PostLoginPlayer(LoginNetworkData ctx, Action<PlayerNetworkData> callback, Action<string> error)=>
            StartCoroutine(APIServer.POST(SerializeData(ctx),
                RequestAddresses.POST_PLAYERS_LOGIN,
                (s) => { ParseData(s, callback); },
                (s) => { TryParseError(s, error); }
             ));

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

        private void TryParseError(string data, Action<string> callback)
        {
            try
            {
                StatusNetworkData status = JsonConvert.DeserializeObject<StatusNetworkData>(data);
                if (status != default(StatusNetworkData))
                {
                    callback?.Invoke(status.message);
                    return;
                }
            }
            catch(Exception e)
            {
                Debug.Log("Cant parse Status, data = " + data);
            }
            callback?.Invoke(data);
        }

        private string SerializeData<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
