using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OFG.ChessPeak
{
    [RequireComponent(typeof(Image))]
    public abstract class ThemeImageHandler : MonoBehaviour
    {
        protected Image _image;

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
            _image = GetComponent<Image>();
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
