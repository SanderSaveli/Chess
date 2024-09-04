using UnityEngine;
using UnityEngine.Tilemaps;
using IUP.Toolkit;

namespace OFG.ChessPeak
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
        [SerializeField][Min(0)] private int _width;
        [SerializeField][Min(0)] private int _height;
        [SerializeField] private bool _isFirstChessSquareBlack = true;

        public Matrix<Figure> Figures => _figures;
        public Tilemap FigureTilemap => _figureTilemap;
        public Matrix<CellBase> Cells => _cells;
        public int width => _width;
        public int height => _height;

        private Matrix<Figure> _figures;
        private Matrix<CellBase> _cells;

        public bool HasWhiteFigure()
        {
            foreach (Figure figure in Figures)
            {
                if ((figure != null) && figure.IsWhite)
                {
                    return true;
                }
            }
            return false;
        }

        public bool InBounds(int i) => _cells.InBounds(i);

        public bool InBounds(Vector2Int position2) => _cells.InBounds(position2);

        public bool OutBounds(Vector2Int position2) => _cells.OutBounds(position2);

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

        public bool NoFigure(Vector2Int position2) => Figures[position2] == null;

        public bool HasFigure(Vector2Int position2) => Figures[position2] != null;

        public bool TryGetFigure(out Figure figure, Vector2Int position2)
        {
            figure = Figures[position2];
            return figure != null;
        }

        public void ChangeFieldSize(Vector2Int size)
        {
            _width = size.x; 
            _height = size.y;
            DestroyOutboundElement(_cellTilemap);
            DestroyOutboundElement(_figureTilemap);
            InitGameField();
        }

        public void SetFigure(Vector2Int position2)
        {

        }

        public void DeleteFigure(Vector2Int position2)
        {

        }

        private void Awake()
        {
            InitGameField();
        }

        private void OnDrawGizmos() => 
            MatrixUtils.DrawChessGizmos(_width, _height, _cellTilemap, Color.green, Color.black);

        private void InitGameField()
        {
            InitMatrixByTilemap(ref _figures, _figureTilemap);
            InitMatrixByTilemap(ref _cells, _cellTilemap);
            FillCells();
        }
        private void InitMatrixByTilemap<T>(ref Matrix<T> matrix, Tilemap tilemap) where T : Component
        {
            matrix = new Matrix<T>(_width, _height);
            foreach (Transform child in tilemap.transform)
            {
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
                    if (_cells.IsChessSquareBlack(i, _isFirstChessSquareBlack))
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
            Vector2Int coordinate = _cells.ToCoordinate(i);
            Vector3Int cellPosition = new(coordinate.x, coordinate.y, 0);
            Vector3 worldPosition = _cellTilemap.CellToWorld(cellPosition);
            GameObject cellObject = Instantiate(
                cellPrefab,
                worldPosition,
                Quaternion.identity,
                _cellTilemap.transform);
            _cells[i] = cellObject.GetComponent<CellBase>();
        }

        private void DestroyOutboundElement(Tilemap tilemap)
        {
            for (int i = 0; i < tilemap.transform.childCount; i++)
            {
                Transform cellTransform = tilemap.transform.GetChild(i);
                Vector2Int cellCoordinate = WorldToPosition2(cellTransform.position);
                if (cellCoordinate.x >= _width || cellCoordinate.y >= _height)
                {
                    DestroyImmediate(cellTransform.gameObject);
                    i--;
                }
            }
        }
    }
}
