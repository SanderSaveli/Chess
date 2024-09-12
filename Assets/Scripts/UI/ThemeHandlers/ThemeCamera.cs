using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace OFG.ChessPeak
{
    [RequireComponent(typeof(Camera))]
    public class ThemeCamera : MonoBehaviour
    {
        [SerializeField] private Volume _volume;
        [SerializeField] private Camera _camera;
        private Vignette vignette;

        private void Start()
        {
            _camera = GetComponent<Camera>();
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
            _camera.backgroundColor = data.backgroundColor;
            if (_volume.profile.TryGet<Vignette>(out vignette))
            {
                vignette.color = new ColorParameter(data.vignetteColor);
            }
        }
    }
}
