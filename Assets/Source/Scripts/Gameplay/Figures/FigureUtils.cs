using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public static class FigureUtils
    {
        public static int AddIfEmpty(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            Vector2Int direction)
        {
            Vector2Int checkedPosition = position + direction;
            if (gameField.InBounds(checkedPosition) && gameField.NoFigure(checkedPosition))
            {
                moves.Add(checkedPosition);
                return 1;
            }
            return 0;
        }

        public static int AddIfEnemy(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor,
            Vector2Int direction)
        {
            Vector2Int checkedPosition = position + direction;
            if (gameField.InBounds(checkedPosition) &&
                gameField.TryGetFigure(out Figure figure, checkedPosition) &&
                figure.IsEnemy(figureColor))
            {
                moves.Add(checkedPosition);
                return 1;
            }
            return 0;
        }

        public static int AddIfEmptyOrEnemy(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor,
            Vector2Int direction)
        {
            Vector2Int checkedPosition = position + direction;
            if (gameField.InBounds(checkedPosition) &&
                    (!gameField.TryGetFigure(out Figure figure, checkedPosition) ||
                        figure.IsEnemy(figureColor)))
            {
                moves.Add(checkedPosition);
                return 1;
            }
            return 0;
        }

        public static int AddIfEmptyOrAlly(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor,
            Vector2Int direction)
        {
            Vector2Int checkedPosition = position + direction;
            if (gameField.InBounds(checkedPosition) &&
                    (!gameField.TryGetFigure(out Figure figure, checkedPosition) ||
                        !figure.IsEnemy(figureColor)))
            {
                moves.Add(checkedPosition);
                return 1;
            }
            return 0;
        }

        public static int AddIfInBounds(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            Vector2Int direction)
        {
            Vector2Int checkedPosition = position + direction;
            if (gameField.InBounds(checkedPosition))
            {
                moves.Add(checkedPosition);
                return 1;
            }
            return 0;
        }
    }
}
