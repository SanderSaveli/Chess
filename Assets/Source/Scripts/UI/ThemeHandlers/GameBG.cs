using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class GameBG : ThemeImageHandler
    {
        protected override void SetTheme(ThemeData data)
        {
            _image.sprite = data.GameBG;
        }
    }
}
