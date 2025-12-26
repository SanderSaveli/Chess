namespace OFG.ChessPeak
{
    public readonly struct SystemLevelGameEndContext
    {
        public readonly string WorldID;
        public readonly int LevelNumber;
        public readonly ILevelManager LevelManager;
        public SystemLevelGameEndContext(string WorldId, int levelNumber, ILevelManager levelManager)
        {
            WorldID = WorldId;
            LevelNumber = levelNumber;
            LevelManager = levelManager;
        }
    }
}
