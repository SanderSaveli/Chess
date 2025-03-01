using TMPro;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class ThemeText : MonoBehaviour
    {
        private TMP_Text _text;
        private ThemeData _themeData;

        [Inject]
        public void Construct(ThemeManager themeManager)
        {
            _themeData = themeManager.actualTheme;
        }
        private void Start()
        {
            _text = GetComponent<TMP_Text>();
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
            _text.color = data.levelViewLockedLevel;
        }
    }
}
