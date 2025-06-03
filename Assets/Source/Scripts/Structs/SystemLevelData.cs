namespace OFG.ChessPeak
{
    public struct SystemLevelData
    {
        public int LevelNumber;
        public LevelProgress State;

        public SystemLevelData(int levelNumber, LevelProgress state)
        {
            LevelNumber = levelNumber;
            State = state;
        }
    }
}
