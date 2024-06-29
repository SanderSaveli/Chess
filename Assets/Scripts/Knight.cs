using CellField2D;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Figure
{
    public override List<IReferedCell> GetMoves(GameField field)
    {
        List<IReferedCell> cellsToMove = new List<IReferedCell>();

        CheckCell(fieldCoordinate + new Vector2Int(2, 1), field, cellsToMove);
        CheckCell(fieldCoordinate + new Vector2Int(2, -1), field, cellsToMove);
        CheckCell(fieldCoordinate + new Vector2Int(1, 2), field, cellsToMove);
        CheckCell(fieldCoordinate + new Vector2Int(1, -2), field, cellsToMove);
        CheckCell(fieldCoordinate + new Vector2Int(-1, 2), field, cellsToMove);
        CheckCell(fieldCoordinate + new Vector2Int(-1, -2), field, cellsToMove);
        CheckCell(fieldCoordinate + new Vector2Int(-2, 1), field, cellsToMove);
        CheckCell(fieldCoordinate + new Vector2Int(-2, -1), field, cellsToMove);

        return cellsToMove;
    }

    private void CheckCell(Vector2Int coordinate, GameField field, List<IReferedCell> cellsToMove)
    {
        if (field.cellField.TryGetCell(coordinate, out IReferedCell cell))
        {
            if (cell.figure != null)
            {
                if (cell.figure._color != _color)
                {
                    cellsToMove.Add(cell);
                    return;
                }
            }
            else
            {
                cellsToMove.Add(cell);
            }
        }
    }
}
