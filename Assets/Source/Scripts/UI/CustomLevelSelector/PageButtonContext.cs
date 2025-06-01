namespace OFG.ChessPeak
{
    public struct PageButtonContext
    {
        public int PageNumber;
        public bool IsActivePage;

        public PageButtonContext(int pageNumber, bool isActivePage)
        {
            PageNumber = pageNumber;
            IsActivePage = isActivePage;
        }
    }
}
