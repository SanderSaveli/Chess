using CellField2D;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Figure
{
    public override List<IReferedCell> GetMoves(GameField field)
    {
        List<IReferedCell> cellsToMove = new List<IReferedCell>();

        CheckDirection(new Vector2Int(0, 1), field, cellsToMove);
        CheckDirection(new Vector2Int(0, -1), field, cellsToMove);
        CheckDirection(new Vector2Int(1, 0), field, cellsToMove);
        CheckDirection(new Vector2Int(-1, 0), field, cellsToMove);

        return cellsToMove;
    }

    private void CheckDirection(Vector2Int direction, GameField field, List<IReferedCell> cellsToMove)
    {
        int i = 1;
        while (true)
        {
            if (field.cellField.TryGetCell(fieldCoordinate + direction * i, out IReferedCell cell))
            {
                if (cell.ownerID != 0)
                {
                    if (cell.figure._color != _color)
                    {
                        cellsToMove.Add(cell);
                    }
                    break;
                }
                else
                {
                    cellsToMove.Add(cell);
                }
            }
            else
            {
                break;
            }
            i++;
        }
    }
}
