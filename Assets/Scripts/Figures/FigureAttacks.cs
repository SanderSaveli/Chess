using System;
using System.Collections.Generic;
using IUP.Toolkit;
using UnityEngine;

namespace OFG.ChessPeak
{
    public static class FigureAttacks
    {
        public static int GetAtacks(
           IList<Vector2Int> attacks,
           Vector2Int position,
           GameField gameField,
           FigureType figureType,
           FigureColor figureColor) => figureType switch
           {
               FigureType.Pawn => GetPawnAtack(attacks, position),
               FigureType.Knight => GetKnightAtack(attacks, position, gameField, figureColor),
               FigureType.Rook => GetRookAtack(attacks, position, gameField, figureColor),
               FigureType.Bishop => GetBishopAtack(attacks, position, gameField, figureColor),
               FigureType.Queen => GetQueenAtack(attacks, position, gameField, figureColor),
               FigureType.King => GetKingAtack(attacks, position, gameField, figureColor),
               _ => throw new ArgumentOutOfRangeException(nameof(FigureType))
           };

        public static int GetPawnAtack(IList<Vector2Int> attacks, Vector2Int position)
        {
            Vector2Int checkedPosition = position + Direction.UpLeft;
            attacks.Add(checkedPosition);
            checkedPosition = position + Direction.UpRight;
            attacks.Add(checkedPosition);
            return 2;
        }

        public static int GetKnightAtack(
            IList<Vector2Int> attacks,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            int counter = 0;
            foreach (Vector2Int direction in Direction.Knight)
            {
                counter += FigureUtils.AddIfInBounds(attacks, position, gameField, direction);
            }
            return counter;
        }

        public static int GetRookAtack(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor) => AddStraight(moves, position, gameField, figureColor);

        public static int GetBishopAtack(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor) => AddDiagonal(moves, position, gameField, figureColor);

        public static int GetQueenAtack(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField,
            FigureColor figureColor)
        {
            int counter = AddStraight(moves, position, gameField, figureColor);
            counter += AddDiagonal(moves, position, gameField, figureColor);
            return counter;
        }


        public static int GetKingAtack(
            IList<Vector2Int> moves,
            Vector2Int position,
            GameField gameField, 
            FigureColor color)
        {
            int counter = 0;
            foreach (Vector2Int direction in Direction.All)
            {
                counter += FigureUtils.AddIfEmptyOrAlly(moves, position, gameField, color, direction);
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
            int counter = 0;
            Vector2Int checkedPosition = position;
            while (true)
            {
                checkedPosition += direction;
                if (gameField.OutBounds(checkedPosition))
                {
                    break;
                }
                if (gameField.TryGetFigure(out Figure otherFigure, checkedPosition))
                {
                    if (otherFigure.IsEnemy(figureColor))
                    {
                        moves.Add(checkedPosition);
                        counter += 1;
                    }
                    else
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
            return counter;
        }
    }
}
