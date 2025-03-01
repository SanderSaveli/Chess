using Zenject;

namespace OFG.ChessPeak
{
    public class ThemeScrollSelector : ScrollSelector
    {
        [Inject]
        public void Construct(ThemeManager themeManager)
        {
            _startSelectedIndex = themeManager.actualThemeIndex;
        }
    }
}


