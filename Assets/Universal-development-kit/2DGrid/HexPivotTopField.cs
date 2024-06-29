using System.Collections.Generic;
using UnityEngine;

namespace CellField2D
{
    public class HexPivotTopField<TCell> : CellField<TCell> where TCell : I2DCell
    {
        protected override List<Vector2Int> GetCellNeighboursOffsets(Vector2Int coordinates)
        {
            List<Vector2Int> neighbourOffsets;

            if (coordinates.y % 2 == 0)
            {
                neighbourOffsets = new List<Vector2Int>
                {
                    new Vector2Int(0, 1),
                    new Vector2Int(1, 0),
                    new Vector2Int(0, -1),
                    new Vector2Int(-1, -1),
                    new Vector2Int(-1, 0),
                    new Vector2Int (-1, 1),
                };
            }
            else
            {
                neighbourOffsets = new List<Vector2Int>
                {
                    new Vector2Int(1, 1),
                    new Vector2Int(1, 0),
                    new Vector2Int(1, -1),
                    new Vector2Int(0, -1),
                    new Vector2Int(-1, 0),
                    new Vector2Int (0, 1),
                };
            }
            return neighbourOffsets;
        }
    }
}
