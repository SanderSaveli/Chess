using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        public IReadOnlyList<LevelListSO> Worlds => _worlds;
        public LevelListSO CurrentLevel => _currentWorld;

        [SerializeField] private List<LevelListSO> _worlds = new();

        private LevelListSO _currentWorld;

        private void Start()
        {
            _currentWorld = _worlds[0];
        }

        public void SelectWorld(LevelListSO world)
        {
            _currentWorld = world;
        }

        public SystemLevelSO GetLevel(int number)
        {
            return _currentWorld.LevelsList[number -1];
        }
    }
}
