namespace OFG.ChessPeak
{
    public interface IGameEndManager
    {
        public IGameEndHandler GameEndHandler { get; }

        public void SetNewGameEndHandler(IGameEndHandler gameEndHandler);
    }
}
