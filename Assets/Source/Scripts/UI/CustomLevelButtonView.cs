using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OFG.ChessPeak
{
    public class CustomLevelButtonView : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private ImageColorByType _background;
        [SerializeField] protected TextMeshProUGUI _label;
        [SerializeField] private Button _button;

        private Color _backgroundColor;

        public string _levelName { get; private set; }

        public event Action<string> Clicked;

        private ThemeData _themeData;

        [Inject]
        public void Construct(ThemeManager themeManager)
        {
            _themeData = themeManager.actualTheme;
        }
        private void Start()
        {
            SubscribeOnEvents();
        }
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        public virtual void UpdateView(string levelName)
        {
            SetLevelName(levelName);
        }

        private void SubscribeOnEvents()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void UnsubscribeFromEvents()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked() => Clicked?.Invoke(_levelName);

        private void SetLevelName(string levelNumber)
        {
            _levelName = levelNumber;
            _label.SetText(_levelName);
        }
    }
}
