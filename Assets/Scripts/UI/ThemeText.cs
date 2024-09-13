using TMPro;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class ThemeText : MonoBehaviour
    {
        private TMP_Text _text;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
            ThemeData data = ThemeManager.instance.actualTheme;
            SetTheme(data);
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
