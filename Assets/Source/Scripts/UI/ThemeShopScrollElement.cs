using Zenject;
using UnityEngine;
using CustomText;
using TreeEditor;

namespace OFG.ChessPeak
{
    public class ThemeShopScrollElement : AnimatedScrollElement, ISlot<PlayerThemeContext>
    {
        [SerializeField] private TextByTableKey _textKey;

        private ThemeManager _themeManager;

        [Inject]
        public void Construct(ThemeManager themeManager)
        {
            _themeManager = themeManager;
        }

        public void Fill(PlayerThemeContext value)
        {
            _textKey.SetText(value.Theme.Name);
            _image.sprite = value.Theme.ThemeShopBG;
            _selectedColor = Color.white;
            _unselectedColor = Color.white;
        }

        public override void Ini(int index, float delay)
        {
            base.Ini(index, delay);
        }
        public override void Ini(int index)
        {
            Ini(index, 0);
        }

        public override void Select()
        {
            base.Select();
            EventInputNewThemeSet ctx = new EventInputNewThemeSet(_index);
            EventBusProvider.EventBus.InvokeEvent(ctx);
        }
    }
}
