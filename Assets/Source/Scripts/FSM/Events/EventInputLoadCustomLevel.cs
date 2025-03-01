namespace OFG.ChessPeak
{
    public readonly struct EventInputLoadCustomLevel
    {
        public EventInputLoadCustomLevel(string fileName) => levelName = fileName;
        public readonly string levelName;
    }
}
