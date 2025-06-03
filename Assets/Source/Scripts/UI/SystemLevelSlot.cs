using CustomText;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak.UI
{
    public class SystemLevelSlot : MonoBehaviour, ISlot<SystemLevelData>
    {
        [Header(H.Components)]
        [SerializeField] private ImageColorByType _background;
        [SerializeField] private ImageColorByType _lockIcon;
        [SerializeField] protected CustomText.CustomText _levelNumberText;
        [SerializeField] private Button _button;

        [Header(H.Params)]
        [Header("Text")]
        [SerializeField] private Custom_ColorStyle _availableText;
        [SerializeField] private Custom_ColorStyle _lockedText;
        [Header("Backgroud")]
        [SerializeField] private Custom_ColorStyle _availableBackground;
        [SerializeField] private Custom_ColorStyle _lockedBackground;
        public int LevelNumber { get; private set; }

        public event Action<int> Clicked;

        private void OnEnable()
        {
            SubscribeOnEvents();
        }
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }
        public void Fill(SystemLevelData value)
        {
            SetLevelNumber(value.LevelNumber);
            switch (value.State)
            {
                case LevelProgress.Locked:
                    SetInterract(false);
                    SetColor(_lockedText, _lockedBackground);
                    break;

                case LevelProgress.Available:
                    SetInterract(true);
                    SetColor(_availableText, _availableBackground);
                    break;

                case LevelProgress.Complete:
                    SetInterract(true);
                    SetColor(_availableText, _availableBackground);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(value.State));
            }
        }

        private void SetInterract(bool isLevelAvailable)
        {
            _lockIcon.enabled = !isLevelAvailable;
            _levelNumberText.enabled = isLevelAvailable;
            _button.interactable = isLevelAvailable;
        }

        private void SetColor(Custom_ColorStyle textColor, Custom_ColorStyle backgroudColor)
        {
            _levelNumberText.ChangeTextColor(textColor);
            _background.ChangeColor(backgroudColor);
        }

        private void SubscribeOnEvents()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void UnsubscribeFromEvents()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked() => Clicked?.Invoke(LevelNumber);

        private void SetLevelNumber(int levelNumber)
        {
            LevelNumber = levelNumber;
            string strLevelnumber = LevelNumber.ToString();
            _levelNumberText.SetText(strLevelnumber);
        }
    }
}
