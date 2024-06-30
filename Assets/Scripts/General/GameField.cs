using UnityEngine;
using UnityEngine.Tilemaps;
using IUP.Toolkit;

namespace OFG.Chess
{
    public sealed class GameField : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Tilemap _cellTilemap;
        [SerializeField] private Tilemap _figureTilemap;

        [Header(H.Prefabs)]
        [SerializeField] private GameObject _whiteCell;
        [SerializeField] private GameObject _blackCell;

        [Header(H.Params)]
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        public Matrix<Figure> Figures => _figures;
        public Matrix<CellBase> Cells => _cells;

        private Matrix<Figure> _figures;
        private Matrix<CellBase> _cells;

        public bool InBounds(int i) => _cells.InBounds(i);

        public bool InBounds(int x, int y) => _cells.InBounds(x, y);

        public bool InBounds(Vector2Int position2) => _cells.InBounds(position2);

        public Vector3 Position2ToWorld(Vector2Int position2)
        {
            Vector3Int position3 = new(position2.x, position2.y, 0);
            return _cellTilemap.GetCellCenterWorld(position3);
        }

        public Vector3 Position3ToWorld(Vector3Int position3) => _cellTilemap.GetCellCenterWorld(position3);

        public Vector2Int WorldToPosition2(Vector3 worldPosition)
        {
            Vector3Int position3 = _cellTilemap.WorldToCell(worldPosition);
            return new Vector2Int(position3.x, position3.y);
        }

        public Vector3Int WorldToPosition3(Vector3 worldPosition) => _cellTilemap.WorldToCell(worldPosition);

        public bool HasFigure(Vector2Int position2)
        {
            return Figures[position2] != null;
        }

        public bool TryGetFigure(out Figure figure, Vector2Int position2)
        {
            figure = Figures[position2];
            return figure != null;
        }

        private void Awake()
        {
            InitMatrixByTilemap(ref _figures, _figureTilemap);
            InitMatrixByTilemap(ref _cells, _cellTilemap);
            FillCells();
        }

        private void OnDrawGizmos() // TODO: clear this piece of shit.
        {
            float sizeX = _cellTilemap.cellSize.x * _width;
            float sizeY = 1.0f;
            float sizeZ = _cellTilemap.cellSize.y * _height;
            Vector3 size = new Vector3(sizeX, sizeY, sizeZ);
            Vector3 center = size / 2.0f;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(center, size);

            Vector3 cellSize = _cellTilemap.cellSize;
            (cellSize.y, cellSize.z) = (cellSize.z, cellSize.y);
            cellSize.y = 0.1f;
            Vector3 cellHalfSize = cellSize / 2.0f;
            int cellsCount = _width * _height;
            for (int i = 0; i < cellsCount; i += 1)
            {
                int xCellPosition = i % _width;
                int yCellPosition = i / _width;
                Vector3Int cellPosition = new(xCellPosition, yCellPosition, 0);
                Vector3 cellWorldPosition = _cellTilemap.CellToWorld(cellPosition);
                cellWorldPosition += cellHalfSize;
                cellWorldPosition.y = 0.0f;
                int yOffset = yCellPosition % 2;
                if (((xCellPosition + yOffset) % 2) == 0)
                {
                    Gizmos.DrawCube(cellWorldPosition, cellSize);
                }
            }
        }

        private void InitMatrixByTilemap<T>(ref Matrix<T> matrix, Tilemap tilemap) where T : Component
        {
            matrix = new Matrix<T>(_width, _height);
            for (int i = 0; i < tilemap.transform.childCount; i += 1)
            {
                Transform child = tilemap.transform.GetChild(i);
                Vector2Int position = WorldToPosition2(child.position);
                matrix[position] = child.GetComponent<T>();
            }
        }

        private void FillCells()
        {
            for (int i = 0; i < _cells.Count; i += 1)
            {
                if (_cells[i] == null)
                {
                    if (_cells.IsChessSquareBlack(i))
                    {
                        InstantiateCell(i, _blackCell);
                    }
                    else
                    {
                        InstantiateCell(i, _whiteCell);
                    }
                }
            }
        }

        private void InstantiateCell(int i, GameObject cellPrefab)
        {
            Vector3Int cellPosition = _cells.CalculatePosition3(i);
            Vector3 worldPosition = _cellTilemap.CellToWorld(cellPosition);
            GameObject cellObject = Instantiate(
                cellPrefab,
                worldPosition,
                Quaternion.identity,
                _cellTilemap.transform);
            _cells[i] = cellObject.GetComponent<CellBase>();
        }
    }
}
