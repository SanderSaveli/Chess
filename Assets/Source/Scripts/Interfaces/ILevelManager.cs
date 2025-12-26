using System.Collections.Generic;

namespace OFG.ChessPeak
{
    public interface ILevelManager
    {
        public IReadOnlyList<LevelListSO> Worlds { get; }
        public LevelListSO CurrentWorld { get; }
        public int CurrentLevel { get; }

        public void SelectWorld(LevelListSO world);
        public SystemLevelGameEndHandler GenerateLevelEndHandler(int LevelNumber);
        public SystemLevelSO GetLevel(int number);
        public void CompleteLevel(int number);
    }
}
