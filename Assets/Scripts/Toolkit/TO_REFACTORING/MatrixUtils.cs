using UnityEngine;
using UnityEngine.Tilemaps;

namespace IUP.Toolkit
{
    public static class MatrixUtils
    {
        public static bool IsChessSquareBlack(int x, int y, bool isFirstBlack = true) =>
            isFirstBlack && ((x + y) % 2 == 0);

        public static bool IsChessSquareBlack(Vector2Int position2, bool isFirstBlack = true) =>
            IsChessSquareBlack(position2.x, position2.y, isFirstBlack);

        public static bool IsChessSquareBlack(Vector3Int position3, bool isFirstBlack = true) =>
            IsChessSquareBlack(position3.x, position3.y, isFirstBlack);

        public static bool IsChessSquareWhite(int x, int y, bool isFirstBlack = true) =>
            isFirstBlack && ((x + y) % 2 != 0);

        public static bool IsChessSquareWhite(Vector2Int position2, bool isFirstBlack = true) =>
            IsChessSquareWhite(position2.x, position2.y, isFirstBlack);

        public static bool IsChessSquareWhite(Vector3Int position3, bool isFirstBlack = true) =>
            IsChessSquareWhite(position3.x, position3.y, isFirstBlack);

        public static int CalculateIndex(int width, int x, int y) =>
            x + (y * width);

        public static int CalculateIndex(int width, Vector2Int position2) =>
            CalculateIndex(width, position2.x, position2.y);

        public static int CalculateIndex(int width, Vector3Int position3) =>
            CalculateIndex(width, position3.x, position3.y);

        public static Vector2Int CalculatePosition2(int width, int i)
        {
            int x = i % width;
            int y = i / width;
            return new Vector2Int(x, y);
        }

        public static Vector3Int CalculatePosition3(int width, int i)
        {
            int x = i % width;
            int y = i / width;
            return new Vector3Int(x, y, 0);
        }

        public static void DrawChessGizmos(
            int width,
            int height,
            Tilemap tilemap,
            Color fieldWireColor,
            Color squareColor,
            float squareHeight = 0.1f,
            bool isFirstBlack = true)
        {
            DrawChessFieldWireGizmos(width, height, tilemap, fieldWireColor);
            DrawChessSquaresGizmos(width, height, tilemap, squareColor, squareHeight, isFirstBlack);
        }

        private static void DrawChessFieldWireGizmos(int width, int height, Tilemap tilemap, Color fieldWireColor)
        {
            Gizmos.color = fieldWireColor;
            Vector3 fieldWorldSize = new()
            {
                x = tilemap.cellSize.x * width,
                y = tilemap.cellSize.x,
                z = tilemap.cellSize.y * height
            };
            Vector3 fieldWorldCenter = tilemap.transform.position + (fieldWorldSize / 2.0f);
            Gizmos.DrawWireCube(fieldWorldCenter, fieldWorldSize);
        }

        private static void DrawChessSquaresGizmos(
            int width,
            int height,
            Tilemap tilemap,
            Color squareColor,
            float squareHeight,
            bool isFirstBlack)
        {
            Gizmos.color = squareColor;
            Vector3 cellSize = new()
            {
                x = tilemap.cellSize.x,
                y = squareHeight,
                z = tilemap.cellSize.y
            };
            int count = width * height;
            for (int i = 0; i < count; i += 1)
            {
                Vector3Int position3 = CalculatePosition3(width, i);
                Vector3 worldPosition = tilemap.GetCellCenterWorld(position3);
                if (IsChessSquareBlack(position3, isFirstBlack))
                {
                    Gizmos.DrawCube(worldPosition, cellSize);
                }
            }
        }
    }
}
