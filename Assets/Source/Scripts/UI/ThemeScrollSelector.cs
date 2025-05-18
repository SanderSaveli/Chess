using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class ThemeScrollSelector : ScrollSelector
    {
        [SerializeField] private ShopThemeFiller _themeFiller;
        private ThemeManager _themeManager;

        [Inject]
        public void Construct(ThemeManager themeManager)
        {
            _startSelectedIndex = themeManager.actualThemeIndex;
            _themeManager = themeManager;
        }

        public new void Start()
        {
            _themeFiller.FillItems(_themeManager.GetThemeContext());
            base.Start();
        }
    }
}


