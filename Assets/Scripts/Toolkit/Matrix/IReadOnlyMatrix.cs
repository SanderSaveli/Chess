using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkit
{
    public interface IReadOnlyMatrix<T> : IReadOnlyList<T>,
        IReadOnlyCollection<T>,
        IEnumerable<T>,
        IEnumerable
    {
        public int Width { get; }
        public int Height { get; }
        public new int Count { get; }

        public T this[Vector2Int coordinate] { get; }

        public bool InBounds(int i);
        public bool InBounds(Vector2Int coordinate);

        public bool OutBounds(int i);
        public bool OutBounds(Vector2Int coordinate);

        public int ToIndex(Vector2Int coordinate);

        public Vector2Int ToCoordinate(int i);
    }
}
