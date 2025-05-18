namespace OFG.ChessPeak
{
    public class ThemeNameNandler : ThemeTextHandler
    {
        protected override void SetTheme(ThemeData data)
        {
            _text.SetText(data.Name);
        }
    }
}
