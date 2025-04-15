using System;

namespace OFG.ChessPeak
{
    public interface INetworkManager
    {
        #region GET
        public void GetCustomLevelList(Action<LevelListNetworkData> callback, Action error);
        public void GetFullCustomLevelData(string id, Action<LevlelNetworkData> callback, Action error);
        public void GetPlayerData(string id, Action<PlayerNetworkData> callback, Action error);
        #endregion

        #region POST
        public void PostCreateNewPlayer(LoginNetworkData ctx, Action<PlayerNetworkData> callback, Action error);
        public void PostLoginPlayer(LoginNetworkData ctx, Action<PlayerNetworkData> callback, Action error);
        public void PostCreateNewLevel(CreateLevelNetworkData ctx, Action<StatusNetworkData> callback, Action error);
        #endregion
    }
}
