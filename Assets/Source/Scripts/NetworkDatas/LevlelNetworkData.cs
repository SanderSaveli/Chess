using System;

namespace OFG.ChessPeak
{
    [Serializable]
    public class LevlelNetworkData
    {
        public string name;
        public LevelData data;

        public LevlelNetworkData(string level_name, LevelData level)
        {
            this.name = level_name;
            this.data = level;
        }
    }
}
