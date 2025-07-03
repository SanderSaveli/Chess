namespace OFG.ChessPeak
{
    public readonly struct SignalThemeChanged
    {
        public readonly ThemeData Theme;

        public SignalThemeChanged(ThemeData theme)
        {
            Theme = theme;
        }
    }
}
