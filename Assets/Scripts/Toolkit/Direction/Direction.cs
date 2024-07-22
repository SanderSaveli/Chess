using UnityEngine;

namespace IUP.Toolkit
{
    public static class Direction
    {
        public static Vector2Int Up => new(0, 1);
        public static Vector2Int Down => new(0, -1);
        public static Vector2Int Left => new(-1, 0);
        public static Vector2Int Right => new(1, 0);

        public static Vector2Int UpLeft => new(-1, 1);
        public static Vector2Int UpRight => new(1, 1);
        public static Vector2Int DownLeft => new(-1, -1);
        public static Vector2Int DownRight => new(1, -1);

        public static Vector2Int KnightUpLeft => new(-1, 2);
        public static Vector2Int KnightUpRight => new(1, 2);
        public static Vector2Int KnightRightUp => new(2, 1);
        public static Vector2Int KnightRightDown => new(2, -1);
        public static Vector2Int KnightDownLeft => new(-1, -2);
        public static Vector2Int KnightDownRight => new(1, -2);
        public static Vector2Int KnightLeftUp => new(-2, 1);
        public static Vector2Int KnightLeftDown => new(-2, -1);

        public static StraightDirections Straight => new();
        public static DiagonalDirections Diagonal => new();
        public static AllDirections All => new();
        public static KnightDirections Knight => new();
    }
}
