using IUP.Toolkit;
using UnityEngine;

namespace OFG.Chess
{
    public class SelectionViewController : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Transform _selectionsParent;

        [Header(H.Params)]
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        [Header(H.Prefabs)]
        [SerializeField] private GameObject _selectionViewPrefab;

        private Matrix<SelectionView> _selectionViews;
        private GameField _gameField;

        public void Init(GameField gameField)
        {
            _gameField = gameField;
            _selectionViews = new Matrix<SelectionView>(_width, _height);
            InstantiateSelections();
        }

        public void ResetAllSelections()
        {
            for (int i = 0; i < _selectionViews.Count; i += 1)
            {
                SelectionView selectionView = _selectionViews[i];
                if (selectionView != null)
                {
                    selectionView.SetSelection(SelectionType.None);
                }
            }
        }

        public SelectionType GetSelection(Vector2Int position) => _selectionViews[position].SelectionType;

        public void ResetSelection(Vector2Int position) => _selectionViews[position].ResetSelection();

        public void SetSelection(Vector2Int position, SelectionType selectionType) =>
            _selectionViews[position].SetSelection(selectionType);

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
            Vector3 worldPosition = _gameField.Position3ToWorld(cellPosition);
            GameObject cellObject = Instantiate(
                _selectionViewPrefab,
                worldPosition,
                Quaternion.identity,
                _selectionsParent);
            _selectionViews[i] = cellObject.GetComponent<SelectionView>();
        }
    }
}
