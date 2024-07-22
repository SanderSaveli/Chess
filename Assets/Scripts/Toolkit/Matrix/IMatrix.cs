using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkit
{
    public interface IMatrix<T> : IReadOnlyMatrix<T>,
        IReadOnlyList<T>,
        IReadOnlyCollection<T>,
        IEnumerable<T>,
        IEnumerable
    {
        public new T this[int i] { get; set; }
        public new T this[Vector2Int coordinate] { get; set; }
    }
}
