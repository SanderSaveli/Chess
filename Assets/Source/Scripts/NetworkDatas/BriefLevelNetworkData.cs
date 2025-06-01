using System;

namespace OFG.ChessPeak
{
    [Serializable]
    public class BriefLevelNetworkData
    {
        public int id;
        public string player_name;
        public string level_name;

        public BriefLevelNetworkData(int id, string player_name, string level_name)
        {
            this.id = id;
            this.player_name = player_name;
            this.level_name = level_name;
        }
    }
}
