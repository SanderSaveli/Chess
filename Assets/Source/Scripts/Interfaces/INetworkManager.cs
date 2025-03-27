using System;

namespace OFG.ChessPeak
{
    public interface INetworkManager
    {
        public void GetCustomLevelList(Action<LevelListNetworkData> callback, Action error);

        public void GetFullCustomLevelData(string id, Action<LevlelNetworkData> callback, Action error);

        public void PostCreateNewLevel(CreateLevelNetworkData ctx, Action<StatusNetworkData> callback, Action error);
    }
}
