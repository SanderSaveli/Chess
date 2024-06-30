using System.Collections.Generic;
using UnityEngine;

namespace CellField2D
{
    public abstract class CellField<TCell> : ICellField<TCell> where TCell : I2DCell
    {
        protected Dictionary<Vector2Int, TCell> _field = new();

        #region GetCell
        public TCell GetCell(int x, int y) => GetCell(new Vector2Int(x, y));

        public TCell GetCell(Vector2Int coordinates) => _field[coordinates];
        public bool TryGetCell(int x, int y, out TCell cell) => TryGetCell(new Vector2Int(x, y), out cell);

        public bool TryGetCell(Vector2Int coordinates, out TCell cell)
        {
            if (FieldContainsCell(coordinates))
            {
                cell = GetCell(coordinates);
                return true;
            }
            cell = default;
            return false;
        }
        #endregion

        #region FieldContainsCell
        public bool FieldContainsCell(TCell cell) => FieldContainsCell(cell.coordinates);

        public bool FieldContainsCell(int x, int y) => FieldContainsCell(new Vector2Int(x, y));

        public bool FieldContainsCell(Vector2Int coordinates) => _field.ContainsKey(coordinates);
        #endregion

        #region GetCellNeighbours
        public List<TCell> GetCellNeighbours(int x, int y) => GetCellNeighbours(new Vector2Int(x, y));

        public List<TCell> GetCellNeighbours(TCell cell) => GetCellNeighbours(cell.coordinates);

        public List<TCell> GetCellNeighbours(Vector2Int coordinates)
        {
            List<TCell> neighbours = new List<TCell>();
            List<Vector2Int> neighbourOffsets = GetCellNeighboursOffsets(coordinates);

            foreach (Vector2Int offset in neighbourOffsets)
            {
                Vector2Int potentialNeighbour = offset + coordinates;
                if (FieldContainsCell(potentialNeighbour))
                {
                    neighbours.Add(GetCell(potentialNeighbour));
                }
            }

            return neighbours;
        }
        #endregion

        #region InstantCell
        public void InstantCell(TCell cell)
        {
            if (_field.ContainsKey(cell.coordinates))
            {
                return;
            }
            _field.Add(cell.coordinates, cell);
        }
        #endregion

        #region IsCellNeighbours
        public bool IsCellNeighbours(TCell ACell, TCell BCell) => IsCellNeighbours(ACell.coordinates, BCell.coordinates);

        public bool IsCellNeighbours(int Ax, int Ay, int Bx, int By) => IsCellNeighbours(new Vector2Int(Ax, Ay), new Vector2Int(Bx, By));

        public bool IsCellNeighbours(Vector2Int Acoordinates, Vector2Int Bcoordinates)
        {
            List<Vector2Int> neighbourOffsets = GetCellNeighboursOffsets(Acoordinates);
            foreach (Vector2Int offset in neighbourOffsets)
            {
                if ((Acoordinates + offset) == Bcoordinates)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region RemoveCell
        public void RemoveCell(int x, int y) => RemoveCell(new Vector2Int(x, y));

        public void RemoveCell(TCell cell) => RemoveCell(cell.coordinates);
        public void RemoveCell(Vector2Int coordinates)
        {
            if (FieldContainsCell(coordinates))
            {
                _field.Remove(coordinates);
            }
        }
        #endregion

        protected abstract List<Vector2Int> GetCellNeighboursOffsets(Vector2Int coordinates);
    }
}
