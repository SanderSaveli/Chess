using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    [Serializable]
    public class LevelListNetworkData
    {
        public List<BriefLevelNetworkData> level_list;

        public string next_page_url;
        public string previous_page_url;
    }
}
