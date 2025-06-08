namespace OFG.ChessPeak
{
    public readonly struct SignalValuteChange
    {
        public readonly int Valute;
        public readonly int ValuteAdd;


        public SignalValuteChange(int value, int valuteAdd)
        {
            Valute = value;
            ValuteAdd = valuteAdd;
        }
    }
}
