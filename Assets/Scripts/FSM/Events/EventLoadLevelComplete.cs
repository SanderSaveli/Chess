namespace OFG.ChessPeak
{
    public readonly struct EventLoadLevelComplete
    {
        public EventLoadLevelComplete(LevelTemplate loadedLevelTemplate) =>
            LoadedLevelTemplate = loadedLevelTemplate;

        public readonly LevelTemplate LoadedLevelTemplate { get; }
    }
}
