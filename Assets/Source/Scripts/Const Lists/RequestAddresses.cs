namespace OFG.ChessPeak
{
    public static class RequestAddresses
    {
        #region GET
        public readonly static string GET_CUSTOM_LEVEL_LIST = "/levels/list?per_page=10&page=1";
        public readonly static string GET_FULL_CUSTOM_LEVEL_DATA = "/levels/get?id={0}";
        public readonly static string GET_PLAYER_DATA = "/players?id={0}";
        #endregion

        #region POST
        public readonly static string POST_CREATE_NEW_LEVEL = "/levels/create";
        public readonly static string POST_PLAYERS_CREATE = "/players/create";
        public readonly static string POST_PLAYERS_LOGIN = "/players/login";
        #endregion
    }
}
