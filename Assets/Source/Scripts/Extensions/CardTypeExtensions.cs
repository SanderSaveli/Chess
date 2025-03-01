using System;

namespace OFG.ChessPeak
{
    public static class CardTypeExtensions
    {
        public static FigureType ToFigureType(this CardType cardType)
        {
            return cardType switch
            {
                CardType.Pawn => FigureType.Pawn,
                CardType.Knight => FigureType.Knight,
                CardType.Bishop => FigureType.Bishop,
                CardType.Rook => FigureType.Rook,
                CardType.Queen => FigureType.Queen,
                CardType.King => FigureType.King,
                _ => throw new ArgumentOutOfRangeException(nameof(cardType))
            };
        }
    }
}
