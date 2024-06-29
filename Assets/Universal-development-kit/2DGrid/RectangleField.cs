using System.Collections.Generic;
using UnityEngine;

namespace CellField2D
{
    public class RectangleField<TCell> : CellField<TCell> where TCell : I2DCell
    {
        protected override List<Vector2Int> GetCellNeighboursOffsets(Vector2Int coordinates)
        {
            return new List<Vector2Int>()
            {
                new Vector2Int(0 ,1),
                new Vector2Int(1 ,1),
                new Vector2Int(1 ,0),
                new Vector2Int(1 ,-1),
                new Vector2Int(0 ,-1),
                new Vector2Int (-1 ,-1),
                new Vector2Int (-1 ,0),
                new Vector2Int (-1 ,1),
            };
        }
    }
}


