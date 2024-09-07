namespace OFG.ChessPeak
{
    public readonly struct EventLoadLevelComplete
    {
        public EventLoadLevelComplete(LevelData loadedLevelTemplate) =>
            LoadedLevelTemplate = loadedLevelTemplate;

        public readonly LevelData LoadedLevelTemplate { get; }
    }
}
