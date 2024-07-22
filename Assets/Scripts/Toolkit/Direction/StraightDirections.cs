using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkit
{
    public readonly struct StraightDirections : IEnumerable<Vector2Int>
    {
        public IEnumerator<Vector2Int> GetEnumerator()
        {
            yield return Direction.Up;
            yield return Direction.Right;
            yield return Direction.Down;
            yield return Direction.Left;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return Direction.Up;
            yield return Direction.Right;
            yield return Direction.Down;
            yield return Direction.Left;
        }
    }
}
