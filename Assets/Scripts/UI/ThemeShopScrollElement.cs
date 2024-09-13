namespace OFG.ChessPeak
{
    public class ThemeShopScrollElement : AnimatedScrollElement
    {
        public override void Ini(int index, float delay)
        {
            base.Ini(index, delay);
            ThemeData themeData = ThemeManager.instance?.themes[index];
            if (themeData != null)
            {
                _text.text = themeData.themeName;
                _selectedColor = themeData.levelViewCurrentLevel;
                _unselectedColor = themeData.levelViewCompletedLevel;

                _backgroundImage.color = SetColorAlfa(_unselectedColor, _backgroundImage.color.a);
            }
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
