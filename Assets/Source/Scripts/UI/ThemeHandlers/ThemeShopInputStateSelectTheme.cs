using Zenject;

namespace OFG.ChessPeak
{
    public class ThemeShopInputStateSelectTheme : ThemeShopInputState
    {
        private ThemeManager _themeManager;

        [Inject]
        public void Construct(ThemeManager themeManager)
        {
            _themeManager = themeManager;
        }
        public ThemeShopInputStateSelectTheme(ThemeShopInputFSM_Context context) : base(context)
        { }

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
