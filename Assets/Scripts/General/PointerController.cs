using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class PointerController : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Camera _camera;

        [Header(H.Params)]
        [SerializeField] private LayerMask _figureLayerMask;
        [SerializeField] private LayerMask _cardLayerMask;

        private GameField _gameField;

        public Ray GetRay() => _camera.ScreenPointToRay(Input.mousePosition);

        public void Init(GameField gameField) => _gameField = gameField;

        public bool TryGetCard(out CardView card, Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _cardLayerMask))
            {
                return hit.collider.TryGetComponent(out card);
            }
            card = null;
            return false;
        }

        public bool TryGetCard(out CardView card)
        {
            Ray ray = GetRay();
            return TryGetCard(out card, ray);
        }

        public Vector3 RayToWorldPosition()
        {
            Ray ray = GetRay();
            return RayToWorldPosition(ray);
        }

        public Vector3 RayToWorldPosition(Ray ray)
        {
            float factor = ray.origin.y / ray.direction.y;
            Vector3 delta = ray.direction * factor;
            return ray.origin - delta;
        }

        public Vector2Int RayToPosition2()
        {
            Ray ray = GetRay();
            return RayToPosition2(ray);
        }

        public Vector2Int RayToPosition2(Ray ray)
        {
            Vector3 worldPosition = RayToWorldPosition(ray);
            return _gameField.WorldToPosition2(worldPosition);
        }

        public Vector3Int RayToPosition3()
        {
            Ray ray = GetRay();
            return RayToPosition3(ray);
        }

        public Vector3Int RayToPosition3(Ray ray)
        {
            Vector3 worldPosition = RayToWorldPosition(ray);
            return _gameField.WorldToPosition3(worldPosition);
        }

        public bool TryGetHoveredCell(out CellBase cell, out Vector2Int position2)
        {
            Ray ray = GetRay();
            position2 = RayToPosition2(ray);
            if (_gameField.InBounds(position2))
            {
                cell = _gameField.Cells[position2];
                return cell != null;
            }
            cell = null;
            position2 = -Vector2Int.one;
            return false;
        }

        public bool TryGetHoveredFigure(out Figure figure, out Vector2Int position2)
        {
            Ray ray = GetRay();
            if (RaycastFigure(out figure, ray))
            {
                _ = TEMP_TryGetFigurePosition(figure, out position2);
                return true;
            }
            else
            {
                position2 = RayToPosition2(ray);
                if (_gameField.InBounds(position2))
                {
                    figure = _gameField.Figures[position2];
                    return figure != null;
                }
            }
            position2 = -Vector2Int.one;
            figure = null;
            return false;
        }

        // TODO: optimized to near to O(1).
        private bool TEMP_TryGetFigurePosition(Figure figure, out Vector2Int position2)
        {
            for (int i = 0; i < _gameField.Figures.Count; i += 1)
            {
                if (_gameField.Figures[i] == figure)
                {
                    position2 = _gameField.Figures.ToCoordinate(i);
                    return true;
                }
            }
            position2 = -Vector2Int.one;
            return false;
        }

        private bool RaycastFigure(out Figure figure, Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _figureLayerMask))
            {
                if (hit.collider.TryGetComponent(out figure))
                {
                    return true;
                }
            }
            figure = null;
            return false;
        }
    }
}
