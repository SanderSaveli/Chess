namespace OFG.ChessPeak
{
    public static class Const
    {
        private static string LOCKAL_BACKEND_SERVER = "http://127.0.0.1:8000/api";

        public static string BACKEND_SERVER = LOCKAL_BACKEND_SERVER;

        #region PrefsKey
        public static string LEVLES_KEY = "Levels/";
        public static string CUSTOM_LEVLES_KEY = "CustomLevels/";
        public static string THEME_KEY = "Themes/";
        public static string TUTORIAL_KEY = "TUTORIALS/";
        public static string PLAYER_ID_KEY = "Player/id";
        public static string VALUTE_KEY = "Player/Valute";
        #endregion

        #region Settings
        public static string SOUND_VOLUME_KEY = "Settings/soundVolume";
        public static string MUSIC_VOLUME_KEY = "Settings/musicVolume";
        public static string SCREENS_ANIMATION_KEY = "Settings/screensAnimation";
        public static string LANGUAGE_KEY = "Settings/language";
        #endregion

        public static string THEME_LEVLE_NAME = "theme_scene";
    }
}
