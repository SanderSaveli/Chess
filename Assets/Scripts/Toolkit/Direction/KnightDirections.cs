using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkit
{
    public readonly struct KnightDirections : IEnumerable<Vector2Int>
    {
        public IEnumerator<Vector2Int> GetEnumerator()
        {
            yield return Direction.KnightUpLeft;
            yield return Direction.KnightUpRight;
            yield return Direction.KnightLeftUp;
            yield return Direction.KnightLeftDown;
            yield return Direction.KnightDownLeft;
            yield return Direction.KnightDownRight;
            yield return Direction.KnightRightUp;
            yield return Direction.KnightRightDown;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return Direction.KnightUpLeft;
            yield return Direction.KnightUpRight;
            yield return Direction.KnightLeftUp;
            yield return Direction.KnightLeftDown;
            yield return Direction.KnightDownLeft;
            yield return Direction.KnightDownRight;
            yield return Direction.KnightRightUp;
            yield return Direction.KnightRightDown;
        }
    }
}
