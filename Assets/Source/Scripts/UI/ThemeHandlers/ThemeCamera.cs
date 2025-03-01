using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace OFG.ChessPeak
{
    [RequireComponent(typeof(Camera))]
    public class ThemeCamera : MonoBehaviour
    {
        [SerializeField] private Volume _volume;
        [SerializeField] private Camera _camera;
        private Vignette vignette;

        private ThemeData _themeData;

        [Inject]
        public void Construct(ThemeManager themeManager)
        {
            _themeData = themeManager.actualTheme;
        }
        private void Start()
        {
            _camera = GetComponent<Camera>();
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
            _camera.backgroundColor = data.backgroundColor;
            if (_volume.profile.TryGet<Vignette>(out vignette))
            {
                vignette.color = new ColorParameter(data.vignetteColor);
            }
        }
    }
}
