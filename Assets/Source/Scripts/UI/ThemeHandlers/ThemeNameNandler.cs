using Unity.VisualScripting;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class ThemeNameNandler : ThemeTextHandler
    {
        [SerializeField] CustomText.CustomText customText;
        protected override void SetTheme(ThemeData data)
        {
            _text.SetText(data.Name);
            if(customText != null)
            {
                customText.ChangeTextColor(customText.TextColor);
            }
        }
    }
}
