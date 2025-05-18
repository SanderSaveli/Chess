using CustomText;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class MessageUiScreen : UIScreen
    {
        [SerializeField] private TextByTableKey _massageText;

        public void UpdateMessage(string key)
        {
            _massageText.SetText(key);
        }
    }
}
