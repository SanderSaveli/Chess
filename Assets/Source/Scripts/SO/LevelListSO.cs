using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    [CreateAssetMenu(fileName = "new LevelList", menuName = "OFG/LevelList")]
    public class LevelListSO : ScriptableObject
    {
        [SerializeField] private string _listID;
        [SerializeField] private List<SystemLevelSO> _levelsList;

        public string ID => _listID;
        public IReadOnlyList<SystemLevelSO> LevelsList => _levelsList;
    }
}
