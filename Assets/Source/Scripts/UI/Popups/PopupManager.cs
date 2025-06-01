using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak.Popup
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private List<PopupData> popupData;

        [SerializeField] private GameObject popupObj;
        private PopUpView popupView;
        private int popupsShowen;

        private void Start()
        {
            popupView = popupObj.GetComponent<PopUpView>();
            if(popupData.Count > 0)
            {
                ShowPopup();
                UpdatePopupContent(popupData[popupsShowen]);
            }
        }
        private void ShowPopup()
        {
            popupObj.SetActive(true);
            popupView.userClicked += UserClickHandler;
        }

        private void HidePopup()
        {
            popupObj.SetActive(false);
        }

        private void UpdatePopupContent(PopupData data)
        {
            popupView.SetTitleText(data.titleText);
            popupView.SetMainText(data.mainText);
            popupsShowen++;
        }

        private void UserClickHandler()
        {
            if (popupsShowen < popupData.Count)
            {
                UpdatePopupContent(popupData[popupsShowen]);
            }
            else
            {
                HidePopup();
            }
        }
    }
}
