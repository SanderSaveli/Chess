using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public static class FigureSelections
    {
        public static void GetSelections(
            IList<Vector2Int> moves,
            IDictionary<Vector2Int, SelectionType> selections,
            GameField gameField,
            FigureColor figureColor)
        {
            foreach (Vector2Int movePosition in moves)
            {
                GetSelection(selections, movePosition, gameField, figureColor);
            }
        }

        private static void GetSelection(
            IDictionary<Vector2Int, SelectionType> selections,
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
                                            otherFigure.IsEnemy(figureColor);
    }
}
