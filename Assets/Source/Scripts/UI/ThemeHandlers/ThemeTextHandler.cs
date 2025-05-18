using CustomText;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public abstract class ThemeTextHandler : MonoBehaviour
    {
        [SerializeField] protected TextByTableKey _text;

        protected ThemeData _themeData;

        [Inject]
        public void Construct(ThemeManager themeManager)
        {
            _themeData = themeManager.actualTheme;
        }
        private void Start()
        {
            SetTheme(_themeData);
        }

        private void OnEnable()
        {
            EventBusProvider.EventBus.RegisterCallback<EventNewThemeSet>(SetTheme);
        }
        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventNewThemeSet>(SetTheme);
        }
        private void SetTheme(EventNewThemeSet data) => SetTheme(data.ThemeData);
        protected abstract void SetTheme(ThemeData data);

    }
}
