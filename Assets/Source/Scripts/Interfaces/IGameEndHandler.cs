namespace OFG.ChessPeak
{
    public interface IGameEndHandler
    {
        public UIScreen WinScreenPrefab { get; }
        public UIScreen LoseScreenPrefab { get; }
        public LevelType Type { get; }
        public void LoadComplete();
        public void GameEnd(bool isWin);
        public void SetScreenContext(ScreensContext ctx);
    }
}
