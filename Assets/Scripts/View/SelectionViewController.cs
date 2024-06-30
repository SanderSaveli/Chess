using IUP.Toolkit;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace OFG.Chess
{
    public class SelectionViewController : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Transform _selectionsParent;

        [Header(H.Params)]
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        [Header(H.Prefabs)]
        [SerializeField] private GameObject _selectionViewPrefab;

        private Matrix<SelectionView> _selectionViews;

        public SelectionType GetSelection(Vector2Int position) => _selectionViews[position].SelectionType;

        public void ResetSelection(Vector2Int position) => _selectionViews[position].ResetSelection();

        public void SetSelection(Vector2Int position, SelectionType selectionType) =>
            _selectionViews[position].SetSelection(selectionType);

        private void Awake()
        {
            _selectionViews = new Matrix<SelectionView>(_width, _height);
            InstantiateSelections();
        }

        private void InstantiateSelections()
        {
            for (int i = 0; i < _selectionViews.Count; i += 1)
            {
                InstantiateSelection(i);
            }
        }

        private void InstantiateSelection(int i)
        {
            Vector3Int cellPosition = _selectionViews.CalculatePosition3(i);
            Vector3 worldPosition = _tilemap.CellToWorld(cellPosition);
            GameObject cellObject = Instantiate(
                _selectionViewPrefab,
                worldPosition,
                Quaternion.identity,
                _selectionsParent);
            _selectionViews[i] = cellObject.GetComponent<SelectionView>();
        }
    }
}
