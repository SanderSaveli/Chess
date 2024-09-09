namespace OFG.ChessPeak
{
    public readonly struct EventLoadLevelComplete
    {
        public EventLoadLevelComplete(LevelData loadedLevelTemplate, int levelNumber)
        {
            LoadedLevelTemplate = loadedLevelTemplate;
            LevelNumber = levelNumber;
        }

        public readonly LevelData LoadedLevelTemplate { get; }
        public readonly int LevelNumber { get; }
    }
}
