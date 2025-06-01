namespace OFG.ChessPeak
{
    public readonly struct SignalInputOpenWindow
    {
        public readonly MenuScreens Screen;
        public SignalInputOpenWindow(MenuScreens screen)
        {
            Screen = screen;
        }
    }
}
