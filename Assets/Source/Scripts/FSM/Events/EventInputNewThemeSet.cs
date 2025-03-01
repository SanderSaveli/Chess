namespace OFG.ChessPeak
{
    public readonly struct EventInputNewThemeSet 
    {
        public EventInputNewThemeSet(int themeIndex) => this.themeIndex = themeIndex;
        public readonly int themeIndex;
    }
}
