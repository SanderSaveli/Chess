using UnityEngine;

namespace OFG.ChessPeak
{
    public class ThemeAnimatedScrollElement : AnimatedScrollElement
    {
        protected override void Start()
        {
            base.Start();
            ThemeData data = ThemeManager.instance.actualTheme;
            SetTheme(data);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            EventBusProvider.EventBus.RegisterCallback<EventNewThemeSet>(SetTheme);
        }
        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventNewThemeSet>(SetTheme);
        }
        private void SetTheme(EventNewThemeSet data) => SetTheme(data.ThemeData);
        private void SetTheme(ThemeData data)
        {
            _selectedColor = data.scrollElementSelected;
            _unselectedColor = data.scrollElementUnselected;
            if (_isSelected)
            {
                _backgroundImage.color = SetColorAlfa(_selectedColor, _backgroundImage.color.a);
            }
            else
            {
                _backgroundImage.color = SetColorAlfa(_unselectedColor, _backgroundImage.color.a);
            }
        }

        protected Color SetColorAlfa(Color color, float a) 
        { 
            color.a = a;
            return color;
        }
    }
}
