using UnityEngine;

namespace IUP.Toolkit
{
    public static class IReadOnlyMatrixExtensions
    {
        public static bool IsChessSquareBlack<T>(this IReadOnlyMatrix<T> matrix, int i, bool isFirstBlack = true)
        {
            Vector2Int position2 = matrix.ToCoordinate(i);
            return matrix.IsChessSquareBlack(position2.x, position2.y, isFirstBlack);
        }

        public static bool IsChessSquareBlack<T>(this IReadOnlyMatrix<T> matrix, int x, int y, bool isFirstBlack = true) =>
            isFirstBlack && ((x + y) % 2 == 0);

        public static bool IsChessSquareBlack<T>(this IReadOnlyMatrix<T> matrix, Vector2Int coordinate, bool isFirstBlack = true) =>
            matrix.IsChessSquareBlack(coordinate.x, coordinate.y, isFirstBlack);

        public static bool IsChessSquareWhite<T>(this IReadOnlyMatrix<T> matrix, int i, bool isFirstBlack = true)
        {
            Vector2Int coordinate = matrix.ToCoordinate(i);
            return matrix.IsChessSquareWhite(coordinate.x, coordinate.y, isFirstBlack);
        }

        public static bool IsChessSquareWhite<T>(this IReadOnlyMatrix<T> matrix, int x, int y, bool isFirstBlack = true) =>
            isFirstBlack && ((x + y) % 2 != 0);

        public static bool IsChessSquareWhite<T>(this IReadOnlyMatrix<T> matrix, Vector2Int coordinate, bool isFirstBlack = true) =>
            matrix.IsChessSquareWhite(coordinate.x, coordinate.y, isFirstBlack);
    }
}
