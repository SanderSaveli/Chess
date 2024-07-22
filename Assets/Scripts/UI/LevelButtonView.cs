using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak.UI
{
    public sealed class LevelButtonView : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Image _background;
        [SerializeField] private Image _lockIcon;
        [SerializeField] private TextMeshProUGUI _levelNumberLabel;
        [SerializeField] private Button _button;

        [Header(H.Styles)]
        [SerializeField] private Color _backgroundColorLocked;
        [SerializeField] private Color _backgroundColorAvailable;
        [SerializeField] private Color _backgroundColorComplete;

        public int LevelNumber { get; private set; }

        public event Action<int> Clicked;

        public void UpdateView(int levelNumber, LevelProgress levelProgress)
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

        private void Awake() => SubscribeOnEvents();

        private void OnDestroy() => UnsubscribeFromEvents();

        private void SubscribeOnEvents() => _button.onClick.AddListener(OnClicked);

        private void UnsubscribeFromEvents() => _button.onClick.RemoveListener(OnClicked);

        private void OnClicked() => Clicked?.Invoke(LevelNumber);

        private void SetLevelNumber(int levelNumber)
        {
            LevelNumber = levelNumber;
            string strLevelnumber = LevelNumber.ToString();
            _levelNumberLabel.SetText(strLevelnumber);
        }
    }
}
