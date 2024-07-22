using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkit
{
    public class Matrix<T> : IMatrix<T>,
        IReadOnlyMatrix<T>,
        IReadOnlyList<T>,
        IReadOnlyCollection<T>,
        IEnumerable<T>,
        IEnumerable
    {
        public Matrix()
        {
            Width = 0;
            Height = 0;
            _array = new T[0];
        }

        public Matrix(int width, int height)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), _negativeMatrixSizeExceptionMessage);
            }
            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), _negativeMatrixSizeExceptionMessage);
            }
            Width = width;
            Height = height;
            _array = new T[Count];
        }

        public int Width { get; }
        public int Height { get; }

        public int Count => Width * Height;

        private const string _negativeMatrixSizeExceptionMessage = "Negative size of matrix does not make sense.";

        public T this[int i]
        {
            get => _array[i];
            set => _array[i] = value;
        }
        public T this[Vector2Int coordinate]
        {
            get
            {
                int i = ToIndex(coordinate);
                return _array[i];
            }
            set
            {
                int i = ToIndex(coordinate);
                _array[i] = value;
            }
        }

        private readonly T[] _array;

        public bool InBounds(int i) => (i >= 0) && (i < Count);

        public bool InBounds(Vector2Int coordinate) =>
            (coordinate.x >= 0) && (coordinate.x < Width) && (coordinate.y >= 0) && (coordinate.y < Height);

        public bool OutBounds(int i) => (i < 0) || (i >= Count);

        public bool OutBounds(Vector2Int coordinate) =>
            (coordinate.x < 0) || (coordinate.x >= Width) || (coordinate.y < 0) || (coordinate.y >= Height);

        public int ToIndex(Vector2Int coordinate) => coordinate.x + (coordinate.y * Width);

        public Vector2Int ToCoordinate(int i)
        {
            int x = i % Width;
            int y = i / Width;
            return new Vector2Int(x, y);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i += 1)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => _array.GetEnumerator();
    }
}
