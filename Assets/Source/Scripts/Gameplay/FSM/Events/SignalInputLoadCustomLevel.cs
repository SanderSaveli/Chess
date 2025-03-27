namespace OFG.ChessPeak
{
    public readonly struct SignalInputLoadCustomLevel
    {
        public SignalInputLoadCustomLevel(string fileName) => levelName = fileName;
        public readonly string levelName;
    }
}
