using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkit
{
    public readonly struct DiagonalDirections : IEnumerable<Vector2Int>
    {
        public IEnumerator<Vector2Int> GetEnumerator()
        {
            yield return Direction.UpRight;
            yield return Direction.DownRight;
            yield return Direction.DownLeft;
            yield return Direction.UpLeft;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return Direction.UpRight;
            yield return Direction.DownRight;
            yield return Direction.DownLeft;
            yield return Direction.UpLeft;
        }
    }
}
