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
        [SerializeField] private Image _background;
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
            SetTheme(_themeData);
            SubscribeOnEvents();
        }
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }
        private void SetTheme(EventNewThemeSet data) => SetTheme(data.ThemeData);
        protected virtual void SetTheme(ThemeData data)
        {
            _backgroundColor = data.levelViewCurrentLevel;
        }

        public virtual void UpdateView(string levelName)
        {
            SetLevelName(levelName);
            _background.color = _backgroundColor;
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

        private void OnClicked() => Clicked?.Invoke(_levelName);

        private void SetLevelName(string levelNumber)
        {
            _levelName = levelNumber;
            _label.SetText(_levelName);
        }
    }
}
