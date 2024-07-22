using System;
using System.Collections.Generic;
using IUP.Toolkit;
using UnityEngine;

namespace OFG.ChessPeak
{
    public static class FigureMoves
    {
        public static int GetMoves(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureType figureType,
            FigureColor figureColor) => figureType switch
            {
                FigureType.Pawn => GetPawnMoves(moves, position, gameField, figureColor),
                FigureType.Knight => GetKnightMoves(moves, position, gameField, figureColor),
                FigureType.Bishop => GetBishopMoves(moves, position, gameField, figureColor),
                FigureType.Rook => GetRookMoves(moves, position, gameField, figureColor),
                FigureType.Queen => GetQueenMoves(moves, position, gameField, figureColor),
                FigureType.King => GetKingMoves(moves, position, gameField, figureColor),
                _ => throw new ArgumentOutOfRangeException(nameof(figureType))
            };

        public static int GetPawnMoves(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            int counter = FigureUtils.AddIfEmpty(moves, position, gameField, Direction.Up);
            counter += FigureUtils.AddIfEnemy(moves, position, gameField, figureColor, Direction.UpLeft);
            counter += FigureUtils.AddIfEnemy(moves, position, gameField, figureColor, Direction.UpRight);
            return counter;
        }

        public static int GetKnightMoves(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            int counter = 0;
            foreach (Vector2Int direction in Direction.Knight)
            {
                counter += FigureUtils.AddIfEmptyOrEnemy(moves, position, gameField, figureColor, direction);
            }
            return counter;
        }

        public static int GetRookMoves(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor) => AddStraight(moves, position, gameField, figureColor);

        public static int GetBishopMoves(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor) => AddDiagonal(moves, position, gameField, figureColor);

        public static int GetQueenMoves(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            int counter = AddStraight(moves, position, gameField, figureColor);
            counter += AddDiagonal(moves, position, gameField, figureColor);
            return counter;
        }

        public static int GetKingMoves(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            int counter = 0;
            foreach (Vector2Int direction in Direction.All)
            {
                counter += FigureUtils.AddIfEmptyOrEnemy(moves, position, gameField, figureColor, direction);
            }
            return counter;
        }

        public static int AddStraight(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            int counter = 0;
            foreach (Vector2Int direction in Direction.Straight)
            {
                counter += AddDirection(moves, position, gameField, figureColor, direction);
            }
            return counter;
        }

        public static int AddDiagonal(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            int counter = 0;
            foreach (Vector2Int direction in Direction.Diagonal)
            {
                counter += AddDirection(moves, position, gameField, figureColor, direction);
            }
            return counter;
        }

        public static int AddDirection(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor,
            Vector2Int direction)
        {
            Vector2Int checkedPosition = position;
            int counter = 0;
            while (true)
            {
                checkedPosition += direction;
                if (gameField.OutBounds(checkedPosition))
                {
                    break;
                }
                else if (gameField.TryGetFigure(out Figure figure, checkedPosition))
                {
                    if (figure.IsEnemy(figureColor))
                    {
                        moves.Add(checkedPosition);
                        counter += 1;
                    }
                    break;
                }
                else
                {
                    moves.Add(checkedPosition);
                    counter += 1;
                }
            }
            return counter;
        }
    }
}
