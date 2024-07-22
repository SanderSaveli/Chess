namespace OFG.ChessPeak
{
    public readonly struct EventInputLoadLevel
    {
        public EventInputLoadLevel(int levelNumber) => LevelNumber = levelNumber;

        public readonly int LevelNumber { get; }
    }
}
