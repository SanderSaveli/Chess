namespace OFG.ChessPeak
{
    public class ThemeScrollElement : AnimatedScrollElement
    {
        public override void Select()
        {
            base.Select();
            EventInputNewThemeSet ctx = new EventInputNewThemeSet(_index);
            EventBusProvider.EventBus.InvokeEvent(ctx);
        }
    }
}
