using System;

namespace OFG.ChessPeak
{
    [Serializable]
    public class LevlelNetworkData
    {
        public string level_name;
        public LevelData level;

        public LevlelNetworkData(string level_name, LevelData level)
        {
            this.level_name = level_name;
            this.level = level;
        }
    }
}
