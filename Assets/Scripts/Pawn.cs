using CellField2D;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Figure
{
    public override List<IReferedCell> GetMoves(GameField field)
    {
        List<IReferedCell> cellsToMove = new List<IReferedCell>();

        if (field.cellField.TryGetCell(fieldCoordinate + new Vector2Int(0, 1), out IReferedCell cell))
        {
            if (cell.figure == null)
            {
                cellsToMove.Add(cell);
            }
        }
        if (field.cellField.TryGetCell(fieldCoordinate + new Vector2Int(1, 1), out IReferedCell cellR))
        {
            if (cellR.figure != null && cellR.figure._color != _color)
                cellsToMove.Add(cellR);
        }
        if (field.cellField.TryGetCell(fieldCoordinate + new Vector2Int(-1, 1), out IReferedCell cellL))
        {
            if (cellL.figure != null && cellL.figure._color != _color)
                cellsToMove.Add(cellL);
        }

        return cellsToMove;
    }
}
