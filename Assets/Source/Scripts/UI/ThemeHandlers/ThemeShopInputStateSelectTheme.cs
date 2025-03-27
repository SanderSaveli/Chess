using Zenject;

namespace OFG.ChessPeak
{
    public class ThemeShopInputStateSelectTheme : ThemeShopInputState
    {
        private ThemeManager _themeManager;

        public ThemeShopInputStateSelectTheme(ThemeShopInputFSM_Context context, ThemeManager themeManager) : base(context)
        {
            _themeManager = themeManager;
        }

        public override void OnEnter()
        {
            EventBusProvider.EventBus.RegisterCallback<EventInputNewThemeSet>(SetTheme);
            base.OnEnter();
        }

        public override void OnExit()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventInputNewThemeSet>(SetTheme);
            base.OnExit();
        }

        private void SetTheme(EventInputNewThemeSet ctx)
        {
            _themeManager.SetNewActualTheme(ctx.themeIndex);
        }
    }
}
