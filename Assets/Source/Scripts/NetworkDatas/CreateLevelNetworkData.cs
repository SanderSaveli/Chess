using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    [Serializable]
    public class CreateLevelNetworkData
    {
        public string name;
        public string player_id;
        public LevelData data;

        public CreateLevelNetworkData(string level_name, string player_id, LevelData level)
        {
            this.name = level_name;
            this.player_id = player_id;
            this.data = level;
        }
    }
}
