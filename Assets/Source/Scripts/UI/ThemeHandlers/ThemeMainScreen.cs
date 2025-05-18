using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OFG.ChessPeak
{
    public class ThemeMainScreen : ThemeImageHandler
    {
        protected override void SetTheme(ThemeData data)
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            _image.sprite = data.mainMenuImage;
        }
    }
}
