using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    [RequireComponent(typeof(Image))]
    public class ThemeMainScreen : MonoBehaviour
    {
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
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
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            _image.sprite = data.mainMenuImage;
        }
    }
}
