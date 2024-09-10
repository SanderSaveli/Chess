namespace OFG.ChessPeak
{
    public class ThemeScrollSelector : ScrollSelector
    {
        private void Awake()
        {
            _startSelectedIndex = ThemeManager.instance.actualThemeIndex;
        }
    }
}


