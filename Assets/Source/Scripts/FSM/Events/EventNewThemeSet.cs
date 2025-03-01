namespace OFG.ChessPeak
{
    public readonly struct EventNewThemeSet
    {
        public EventNewThemeSet(ThemeData themeData) => ThemeData = themeData;
        public readonly ThemeData ThemeData;
    }
}
