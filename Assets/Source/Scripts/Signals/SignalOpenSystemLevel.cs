namespace OFG.ChessPeak
{
    public readonly struct SignalOpenSystemLevel
    {
        public readonly SystemLevelGameEndContext Ctx;

        public SignalOpenSystemLevel(SystemLevelGameEndContext ctx)
        {
            Ctx = ctx;
        }
    }
}
