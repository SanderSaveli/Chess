using System;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.Chess
{
    public static class FigureMoves
    {
        public static void GetMoves(
            ref List<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureType figureType,
            FigureColor figureColor)
        {
            switch (figureType)
            {
                case FigureType.Pawn:
                    GetPawnMoves(ref moves, position, gameField, figureColor);
                    break;

                case FigureType.Knight:
                    GetKnightMoves(ref moves, position, gameField, figureColor);
                    break;

                case FigureType.Bishop:
                    GetBishopMoves(ref moves, position, gameField, figureColor);
                    break;

                case FigureType.Rook:
                    GetRookMoves(ref moves, position, gameField, figureColor);
                    break;

                case FigureType.Queen:
                    GetQueenMoves(ref moves, position, gameField, figureColor);
                    break;

                case FigureType.King:
                    GetKingMoves(ref moves, position, gameField, figureColor);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(figureType));
            }
        }

        public static void GetPawnMoves(
            ref List<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            AddIfEmpty(ref moves, position, gameField, Vector2Int.up);
            AddIfEnemy(ref moves, position, gameField, figureColor, new Vector2Int(1, 1));
            AddIfEnemy(ref moves, position, gameField, figureColor, new Vector2Int(-1, 1));
        }

        public static void GetRookMoves(
            ref List<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            AddStraight(ref moves, position, gameField, figureColor);
        }

        public static void GetBishopMoves(
            ref List<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            AddDiagonal(ref moves, position, gameField, figureColor);
        }

        public static void GetKnightMoves(
            ref List<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(2, 1));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(2, -1));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(1, 2));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(1, -2));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(-1, 2));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(-1, -2));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(-2, 1));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(-2, -1));
        }

        public static void GetQueenMoves(
            ref List<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            AddStraight(ref moves, position, gameField, figureColor);
            AddDiagonal(ref moves, position, gameField, figureColor);
        }

        public static void GetKingMoves(
            ref List<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(0, 1));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(1, 0));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(1, 1));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(0, -1));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(-1, 0));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(-1, -1));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(-1, 1));
            AddIfEmptyOrEnemy(ref moves, position, gameField, figureColor, new Vector2Int(1, -1));
        }

        public static void AddIfEmpty(
            ref List<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            Vector2Int direction)
        {
            Vector2Int checkedPosition = position + direction;
            if (gameField.InBounds(checkedPosition) &&
                !gameField.HasFigure(checkedPosition))
            {
                moves.Add(checkedPosition);
            }
        }

        public static void AddIfEnemy(
            ref List<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor,
            Vector2Int direction)
        {
            Vector2Int checkedPosition = position + direction;
            if (gameField.InBounds(checkedPosition) &&
                gameField.TryGetFigure(out Figure figure, checkedPosition) &&
                figure.FigureColor != figureColor)
            {
                moves.Add(checkedPosition);
            }
        }

        public static void AddIfEmptyOrEnemy(
            ref List<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor,
            Vector2Int direction)
        {
            Vector2Int checkedPosition = position + direction;
            if (gameField.InBounds(checkedPosition) &&
                    (!gameField.HasFigure(checkedPosition) ||
                        (gameField.TryGetFigure(out Figure otherFigure, checkedPosition) &&
                            (otherFigure.FigureColor != figureColor))))
            {
                moves.Add(checkedPosition);
            }
        }

        public static void AddStraight(
            ref List<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            AddDirection(ref moves, position, gameField, figureColor, new Vector2Int(0, 1));
            AddDirection(ref moves, position, gameField, figureColor, new Vector2Int(0, -1));
            AddDirection(ref moves, position, gameField, figureColor, new Vector2Int(1, 0));
            AddDirection(ref moves, position, gameField, figureColor, new Vector2Int(-1, 0));
        }

        public static void AddDiagonal(
            ref List<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            AddDirection(ref moves, position, gameField, figureColor, new Vector2Int(1, 1));
            AddDirection(ref moves, position, gameField, figureColor, new Vector2Int(1, -1));
            AddDirection(ref moves, position, gameField, figureColor, new Vector2Int(-1, 1));
            AddDirection(ref moves, position, gameField, figureColor, new Vector2Int(-1, -1));
        }

        public static void AddDirection(
            ref List<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor,
            Vector2Int direction)
        {
            Vector2Int checkedPosition = position;
            while (true)
            {
                checkedPosition += direction;
                if (!gameField.InBounds(checkedPosition))
                {
                    break;
                }
                if (gameField.TryGetFigure(out Figure otherFigure, checkedPosition))
                {
                    if (otherFigure.FigureColor != figureColor)
                    {
                        moves.Add(checkedPosition);
                        break;
                    }
                }
                else
                {
                    moves.Add(checkedPosition);
                }
            }
        }
    }
}
