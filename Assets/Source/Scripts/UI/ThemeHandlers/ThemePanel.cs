using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class ThemePanel : ThemeImageHandler
    {
        protected override void SetTheme(ThemeData data)
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            _image.sprite = data.MainMenuPanel;
        }
    }
}
