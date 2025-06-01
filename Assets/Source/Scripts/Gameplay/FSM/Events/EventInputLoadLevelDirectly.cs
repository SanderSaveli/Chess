namespace OFG.ChessPeak
{
    public readonly struct EventInputLoadLevelDirectly
    {
        public EventInputLoadLevelDirectly(LevelData levelData, int levelIndex = -1)
        {
            LevelData = levelData;
            LevelIndex = levelIndex;
        }

        public readonly LevelData LevelData;
        public readonly int LevelIndex;
    }
}
