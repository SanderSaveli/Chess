namespace OFG.ChessPeak
{
    public interface IValuteManager
    {
        public int CurrentValute { get; }

        public void AddValute(int valute);
        public bool TrySpendValute(int cost);
    }
}
