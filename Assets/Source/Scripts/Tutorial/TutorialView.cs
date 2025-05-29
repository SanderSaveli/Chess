using CustomText;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class TutorialView : UIScreen
    {
        [SerializeField] private TextByTableKey _titletext;
        [SerializeField] private Button _button;

        public Action OnButtonClecked;

        public void ShowTutorial(TutorialPopupData popup)
        {
            _titletext.SetText(popup.Description);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(InvokeAction);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(InvokeAction);
        }

        private void InvokeAction()
        {
            OnButtonClecked?.Invoke();
        }
    }
}
