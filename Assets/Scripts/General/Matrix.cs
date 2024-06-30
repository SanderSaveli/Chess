using UnityEngine;

namespace IUP.Toolkit
{
    public sealed class Matrix<T>
    {
        public Matrix(int width, int height)
        {
            Width = width;
            Height = height;
            _elements = new T[Count];
        }

        public int Width { get; }
        public int Height { get; }
        public int Count => Width * Height;

        public T this[int i]
        {
            get => _elements[i];
            set => _elements[i] = value;
        }
        public T this[int x, int y]
        {
            get
            {
                int i = CalculateIndex(x, y);
                return _elements[i];
            }
            set
            {
                int i = CalculateIndex(x, y);
                _elements[i] = value;
            }
        }
        public T this[Vector2Int position2]
        {
            get
            {
                int i = CalculateIndex(position2);
                return _elements[i];
            }
            set
            {
                int i = CalculateIndex(position2);
                _elements[i] = value;
            }
        }

        private readonly T[] _elements;

        public bool InBounds(int i) => i >= 0 && i < Count;

        public bool InBounds(int x, int y) => (x >= 0) && (x < Width) && (y >= 0) && (y < Height);

        public bool InBounds(Vector2Int position2) => InBounds(position2.x, position2.y);

        public int CalculateIndex(int x, int y) => x + (y * Width);

        public int CalculateIndex(Vector2Int position2) => CalculateIndex(position2.x, position2.y);

        public Vector2Int CalculatePosition2(int i)
        {
            int x = i % Width;
            int y = i / Width;
            return new Vector2Int(x, y);
        }

        public Vector3Int CalculatePosition3(int i)
        {
            int x = i % Width;
            int y = i / Width;
            return new Vector3Int(x, y, 0);
        }

        public bool IsChessSquareBlack(int i)
        {
            int x = i % Width;
            int yOffset = i / Width % 2;
            return ((x + yOffset) % 2) == 0;
        }
    }
}
