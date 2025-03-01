using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OFG.ChessPeak
{
    public class ThemeMasc : MonoBehaviour
    {
        private Image _image;
        private ThemeData _themeData;

        [Inject]
        public void Construct(ThemeManager themeManager)
        {
            _themeData = themeManager.actualTheme;
        }
        private void Start()
        {
            _image = GetComponent<Image>();
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
        private void SetTheme(ThemeData data)
        {
            _image.material = data.mascMaterial;
        }
    }
}
