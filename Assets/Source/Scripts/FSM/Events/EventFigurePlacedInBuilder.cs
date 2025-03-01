using UnityEngine;

namespace OFG.ChessPeak
{
    public readonly struct EventFigurePlacedInBuilder
    {
        public EventFigurePlacedInBuilder(Vector2Int position2) => Position2 = position2;

        public readonly Vector2Int Position2 { get; }
    }
}
