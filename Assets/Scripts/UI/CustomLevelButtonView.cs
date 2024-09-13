using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class CustomLevelButtonView : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Image _background;
        [SerializeField] protected TextMeshProUGUI _label;
        [SerializeField] private Button _button;

        private Color _backgroundColor;

        public string _levelName { get; private set; }

        public event Action<string> Clicked;

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
