using System;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OFG.ChessPeak.UI
{
    public class LevelButtonView : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private ImageColorByType _background;
        [SerializeField] private ImageColorByType _lockIcon;
        [SerializeField] protected TextMeshProUGUI _levelNumberLabel;
        [SerializeField] private Button _button;

        public int LevelNumber { get; private set; }

        public event Action<int> Clicked;

        private ThemeData _themeData;

        [Inject]
        public void Construct(ThemeManager themeManager)
        {
            _themeData = themeManager.actualTheme;
        }
        private void OnEnable()
        {
            SubscribeOnEvents();
        }
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        public virtual void UpdateView(int levelNumber, LevelProgress levelProgress)
        {
            SetLevelNumber(levelNumber);
            switch (levelProgress)
            {
                case LevelProgress.Locked:
                    _lockIcon.enabled = true;
                    _levelNumberLabel.enabled = false;
                    _background.ChangeColor(CustomText.Custom_ColorStyle.LevelMenu_NotPassed);
                    _button.interactable = false;
                    break;

                case LevelProgress.Available:
                    _lockIcon.enabled = false;
                    _levelNumberLabel.enabled = true;
                    _background.ChangeColor(CustomText.Custom_ColorStyle.LevelMenu_PassedBackground);
                    _button.interactable = true;
                    break;

                case LevelProgress.Complete:
                    _lockIcon.enabled = false;
                    _levelNumberLabel.enabled = true;
                    _background.ChangeColor(CustomText.Custom_ColorStyle.LevelMenu_PassedBackground);
                    _button.interactable = true;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(levelProgress));
            }
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
            _levelNumberLabel.SetText(strLevelnumber);
        }
    }
}
