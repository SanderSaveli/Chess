using CellField2D;
using System.Collections.Generic;
using UnityEngine;

public class BlackKing : MonoBehaviour
{
    [SerializeField] private GameField gameField;

    [SerializeField] private Figure figure;

    public void MakeKingMove()
    {
        List<IReferedCell> cellsToMove = figure.GetMoves(gameField);

        List<Vector2Int> atackedCells = gameField.GetAtackedCells(FigureColor.White);

        Debug.Log(cellsToMove.Count);
        Debug.Log(atackedCells.Count);
        
        foreach (var cell in cellsToMove)
        {
            if (!atackedCells.Contains(cell.coordinates))
            {
                Debug.Log("Okay");
                gameField.PlaceFigure(figure, cell);
                return;
            }
        }

        if(atackedCells.Contains(figure.fieldCoordinate))
        {
            Debug.Log("мат");
        }
        else
        {
            Debug.Log("пат");
        }

    }
}
