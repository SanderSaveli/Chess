using System.Collections.Generic;

namespace OFG.ChessPeak
{
    public interface ILevelManager
    {
        public IReadOnlyList<LevelListSO> Worlds { get; }
        public LevelListSO CurrentLevel { get; }

        public void SelectWorld(LevelListSO world);

        public SystemLevelSO GetLevel(int number);
    }
}
