using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkit
{
    public class ReadOnlyMatrix<T> : IReadOnlyMatrix<T>,
        IReadOnlyList<T>,
        IReadOnlyCollection<T>,
        IEnumerable<T>,
        IEnumerable
    {
        public ReadOnlyMatrix(IMatrix<T> matrix) => _matrix = matrix;

        public int Width => _matrix.Width;
        public int Height => _matrix.Height;
        public int Count => _matrix.Count;

        protected IMatrix<T> Matrix => _matrix;

        private readonly IMatrix<T> _matrix;

        public T this[int index] => _matrix[index];
        public T this[Vector2Int coordinate] => _matrix[coordinate];

        public bool InBounds(int i) => _matrix.InBounds(i);

        public bool InBounds(Vector2Int coordinate) => _matrix.InBounds(coordinate);

        public bool OutBounds(int i) => _matrix.OutBounds(i);

        public bool OutBounds(Vector2Int coordinate) => _matrix.OutBounds(coordinate);

        public int ToIndex(Vector2Int coordinate) => _matrix.ToIndex(coordinate);

        public Vector2Int ToCoordinate(int i) => _matrix.ToCoordinate(i);

        public IEnumerator<T> GetEnumerator() => _matrix.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_matrix).GetEnumerator();
    }
}
