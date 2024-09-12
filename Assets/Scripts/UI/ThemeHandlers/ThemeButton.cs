using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    [RequireComponent(typeof(Image))]
    public class ThemeButton : MonoBehaviour
    {
        [SerializeField] private ButtonType _type;
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
            if(_image == null)
            {
                _image = GetComponent<Image>();
            }
            switch (_type)
            {
                case ButtonType.positive:
                    _image.color = data.positiveButtonColor;
                    break;
                case ButtonType.negative:
                    _image.color = data.negativeButtonColor;
                    break;
                case ButtonType.neutral:
                    _image.color = data.neutralButtonColor;
                    break;

            }
        }
    }

    enum ButtonType
    {
        positive,
        negative,
        neutral
    }
}
