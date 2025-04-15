namespace OFG.ChessPeak
{
    public static class RequestAddresses
    {
        #region GET
        public readonly static string GET_CUSTOM_LEVEL_LIST = "";
        public readonly static string GET_FULL_CUSTOM_LEVEL_DATA = "";
        public readonly static string GET_PLAYER_DATA = "/players?id={0}";
        #endregion

        #region POST
        public readonly static string POST_CREATE_NEW_LEVEL = "";
        public readonly static string POST_PLAYERS_CREATE = "/players/create";
        public readonly static string POST_PLAYERS_LOGIN = "/players/login";
        #endregion
    }
}
