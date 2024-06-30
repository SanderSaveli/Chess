using System.Collections.Generic;
using UnityEngine;

namespace OFG.Chess
{
    public static class FigureSelections
    {
        public static void GetSelections(
            ref List<Vector2Int> moves,
            ref Dictionary<Vector2Int, SelectionType> selections,
            GameField gameField,
            FigureColor figureColor)
        {
            foreach (Vector2Int movePosition in moves)
            {
                GetSelection(ref selections, movePosition, gameField, figureColor);
            }
        }

        private static void GetSelection(
            ref Dictionary<Vector2Int, SelectionType> selections,
            Vector2Int movePosition,
            GameField gameField,
            FigureColor figureColor)
        {
            if (IsEnemy(movePosition, gameField, figureColor))
            {
                selections.Add(movePosition, SelectionType.Attack);
            }
            else
            {
                selections.Add(movePosition, SelectionType.Movement);
            }
        }

        public static bool IsEnemy(
            Vector2Int movePosition,
            GameField gameField,
            FigureColor figureColor) => gameField.TryGetFigure(out Figure otherFigure, movePosition) &&
                                            otherFigure.FigureColor != figureColor;
    }
}
