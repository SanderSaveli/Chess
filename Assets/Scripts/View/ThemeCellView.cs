using UnityEngine;

namespace OFG.ChessPeak
{
    public class ThemeCellView : MonoBehaviour
    {
        [SerializeField] private FigureColor _color;
        private MeshRenderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
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
            if (_renderer == null)
            {
                _renderer = GetComponent<MeshRenderer>();
            }
            switch (_color)
            {
                case FigureColor.White:
                    _renderer.material = data.whiteCellMaterial;
                    break;
                case FigureColor.Black:
                    _renderer.material = data.blackCellMaterial;
                    break;
            }
        }
    }
}
