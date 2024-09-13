using System;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak.UI
{
    public class LevelButtonView : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Image _background;
        [SerializeField] private Image _lockIcon;
        [SerializeField] protected TextMeshProUGUI _levelNumberLabel;
        [SerializeField] private Button _button;

        private Color _backgroundColorLocked;
        private Color _backgroundColorAvailable;
        private Color _backgroundColorComplete;

        public int LevelNumber { get; private set; }

        public event Action<int> Clicked;

        private void Awake()
        {
            ThemeData data = ThemeManager.instance.actualTheme;
            SetTheme(data);
            SubscribeOnEvents();
        }
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }
        private void SetTheme(EventNewThemeSet data) => SetTheme(data.ThemeData);
        protected virtual void SetTheme(ThemeData data)
        {
            _backgroundColorAvailable = data.levelViewCompletedLevel;
            _backgroundColorLocked = data.levelViewLockedLevel;
            _backgroundColorComplete = data.levelViewCurrentLevel;
        }

        public virtual void UpdateView(int levelNumber, LevelProgress levelProgress)
        {
            SetLevelNumber(levelNumber);
            switch (levelProgress)
            {
                case LevelProgress.Locked:
                    _lockIcon.enabled = true;
                    _levelNumberLabel.enabled = false;
                    _background.color = _backgroundColorLocked;
                    break;

                case LevelProgress.Available:
                    _lockIcon.enabled = false;
                    _levelNumberLabel.enabled = true;
                    _background.color = _backgroundColorAvailable;
                    break;

                case LevelProgress.Complete:
                    _lockIcon.enabled = false;
                    _levelNumberLabel.enabled = true;
                    _background.color = _backgroundColorComplete;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(levelProgress));
            }
        }

        private void SubscribeOnEvents()
        {
            _button.onClick.AddListener(OnClicked);
            EventBusProvider.EventBus.RegisterCallback<EventNewThemeSet>(SetTheme);
        }

        private void UnsubscribeFromEvents()
        {
            _button.onClick.RemoveListener(OnClicked);
            EventBusProvider.EventBus.UnregisterCallback<EventNewThemeSet>(SetTheme);
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
