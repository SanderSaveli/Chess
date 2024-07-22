using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkit
{
    public readonly struct AllDirections : IEnumerable<Vector2Int>
    {
        public IEnumerator<Vector2Int> GetEnumerator()
        {
            yield return Direction.Up;
            yield return Direction.UpRight;
            yield return Direction.Right;
            yield return Direction.DownRight;
            yield return Direction.Down;
            yield return Direction.DownLeft;
            yield return Direction.Left;
            yield return Direction.UpLeft;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return Direction.Up;
            yield return Direction.UpRight;
            yield return Direction.Right;
            yield return Direction.DownRight;
            yield return Direction.Down;
            yield return Direction.DownLeft;
            yield return Direction.Left;
            yield return Direction.UpLeft;
        }
    }
}
