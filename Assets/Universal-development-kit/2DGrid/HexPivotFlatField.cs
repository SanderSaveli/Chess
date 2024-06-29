using CellField2D;
using System.Collections.Generic;
using UnityEngine;

public class HexPivotFlatField<TCell> : CellField<TCell> where TCell : I2DCell
{
    protected override List<Vector2Int> GetCellNeighboursOffsets(Vector2Int coordinates)
    {
        List<Vector2Int> neighbourOffsets;

        if (coordinates.x % 2 == 0)
        {
            neighbourOffsets = new List<Vector2Int>
                {
                    new Vector2Int(1 ,0),
                    new Vector2Int(1 ,1),
                    new Vector2Int(0 ,1),
                    new Vector2Int(-1, 0),
                    new Vector2Int(0, -1),
                    new Vector2Int (1, -1),
                };
        }
        else
        {
            neighbourOffsets = new List<Vector2Int>
                {
                    new Vector2Int(1 ,0),
                    new Vector2Int(0, 1),
                    new Vector2Int(-1, 1),
                    new Vector2Int(-1, 0),
                    new Vector2Int(-1, -1),
                    new Vector2Int (0, -1),
                };
        }
        return neighbourOffsets;
    }
}
