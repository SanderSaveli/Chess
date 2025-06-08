namespace OFG.ChessPeak
{
    public class CustomLevelGameEndHandler : GameEndHandler
    {
        private CustomLevelContext _context;

        public CustomLevelGameEndHandler(CustomLevelContext ctx)
        {
            _context = ctx;
        }
    }
}
