using CustomText;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class PageSlot : MonoBehaviour, ISlot<PageButtonContext>
    {
        public Action<int> OnLevelClick;

        [Header(H.Components)]
        [SerializeField] private CustomText.CustomText _text;
        [SerializeField] private Button _button;
        [SerializeField] private ImageColorByType _color;

        [Header(H.Params)]
        [Header("Active")]
        [SerializeField] private Custom_ColorStyle _activeBackgroundColor;
        [SerializeField] private Custom_ColorStyle _activeTextColor;

        [Header("Inactive")]
        [SerializeField] private Custom_ColorStyle _inactiveBackgroundColor;
        [SerializeField] private Custom_ColorStyle _inactiveTextColor;

        private int _page;

        private void OnEnable()
        {
            _button.onClick.AddListener(InvokeAction);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(InvokeAction);
        }

        public void Fill(PageButtonContext value)
        {
            _page = value.PageNumber;
            _text.text = _page.ToString();
            if(value.IsActivePage)
            {
                _text.ChangeTextColor(_activeTextColor);
                _color.ChangeColor(_activeBackgroundColor);
            }
            else
            {
                _text.ChangeTextColor(_inactiveTextColor);
                _color.ChangeColor(_inactiveBackgroundColor);
            }
        }

        private void InvokeAction()
        {
            OnLevelClick?.Invoke(_page);
        }
    }
}
