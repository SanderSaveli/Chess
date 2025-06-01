namespace OFG.ChessPeak
{
    public struct LevelListContext
    {
        public readonly int PerPage;
        public readonly int Page;

        public LevelListContext(int perPage, int page)
        {
            PerPage = perPage;
            Page = page;
        }
    }
}
