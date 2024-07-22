using UnityEngine;

namespace OFG.ChessPeak.Popup
{
    public class PopupTest : MonoBehaviour
    {
        public string mainText;
        public string titleText;
        public PopUpView view;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                view.SetMainText(mainText);
                view.SetTitleText(titleText);
            }
        }
    }
}
