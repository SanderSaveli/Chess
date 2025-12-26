using Newtonsoft.Json;
using System;
using UnityEngine;

namespace OFG.ChessPeak
{
    [Serializable]
    public class SystemLevelSO
    {
        [SerializeField] private TextAsset _levelJSON;
        [SerializeField] private int _award;

        public TextAsset LevelJSON => _levelJSON;
        public int Award => _award;

        public LevelData GetData()
        {
            return JsonConvert.DeserializeObject<LevelData>(_levelJSON.text);
        }
    }
}
