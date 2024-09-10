using UnityEngine;

namespace OFG.ChessPeak
{
    public class LevelDecore : MonoBehaviour
    {
        [SerializeField] private GameObject _mountains;
        [SerializeField] private GameObject _deck;


        private MeshRenderer _mountainsRenderer;
        private MeshRenderer _fieldRenderer;
        private Transform _transform;
        private const float _scaleForOneCell = 0.125f;

        private void Start()
        {
            ThemeData data = ThemeManager.instance.actualTheme;
            _mountainsRenderer = _mountains.GetComponent<MeshRenderer>();
            _fieldRenderer = _deck.GetComponent<MeshRenderer>();
            SetTheme(data);
        }

        private void OnEnable()
        {
            _transform = GetComponent<Transform>();
            EventBusProvider.EventBus.RegisterCallback<EventNewThemeSet>(SetTheme);
        }

        public void ScaleDecoreForFieldSize(Vector2Int fieldSize)
        {
            Vector3 newScale = Vector3.one;
            newScale.x = fieldSize.x * _scaleForOneCell;
            newScale.y = fieldSize.y * _scaleForOneCell * 2;
            newScale.z = fieldSize.y * _scaleForOneCell;
            _transform.localScale = newScale;
        }
        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventNewThemeSet>(SetTheme);
        }
        private void SetTheme(EventNewThemeSet data) => SetTheme(data.ThemeData);
        private void SetTheme(ThemeData data)
        {
            if (data.mountainsMaterial == null)
            {
                _mountains.SetActive(false);
            }
            else
            {
                _mountains.SetActive(true);
                _mountainsRenderer.material = data.mountainsMaterial;
            }
            _fieldRenderer.material = data.deckMaterial;
        }
    }
}
