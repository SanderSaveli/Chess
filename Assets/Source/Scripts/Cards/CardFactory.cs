using System;
using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class CardFactory : MonoBehaviour
    {
        [Header(H.Prefabs)]
        [SerializeField] private GameObject _cardViewPrefab;

        [Header(H.Styles)]
        [SerializeField] private Sprite _spritePawnCard;
        [SerializeField] private Sprite _spriteKnightCard;
        [SerializeField] private Sprite _spriteBishopCard;
        [SerializeField] private Sprite _spriteRookCard;
        [SerializeField] private Sprite _spriteQueenCard;
        [SerializeField] private Sprite _spriteKingCard;

        public void CreateCardView(CardType cardType)
        {
            GameObject cardViewObject = Instantiate(_cardViewPrefab);
            CardView cardView = cardViewObject.GetComponent<CardView>();
        }

        private Sprite SpriteByCardType(CardType cardType) => cardType switch
        {
            CardType.Pawn => _spritePawnCard,
            CardType.Knight => _spriteKnightCard,
            CardType.Bishop => _spriteBishopCard,
            CardType.Rook => _spriteRookCard,
            CardType.Queen => _spriteQueenCard,
            CardType.King => _spriteKingCard,
            _ => throw new ArgumentOutOfRangeException(nameof(cardType))
        };
    }
}
