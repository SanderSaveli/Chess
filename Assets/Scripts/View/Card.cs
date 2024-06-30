using System;
using UnityEngine;

namespace OFG.Chess
{
    public sealed class Card : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Header(H.Styles)]
        [SerializeField] private Sprite _spritePawnCard;
        [SerializeField] private Sprite _spriteKnightCard;

        [SerializeField] private Sprite _spriteBishopCard;
        [SerializeField] private Sprite _spriteRookCard;
        [SerializeField] private Sprite _spriteQueenCard;
        [SerializeField] private Sprite _spriteKingCard;

        public CardType CardType { get; private set; }

        public void HideCard()
        {
            CardType = CardType.None;
            _spriteRenderer.enabled = false;
        }

        public void InitCard(CardType cardType)
        {
            CardType = cardType;
            _spriteRenderer.sprite = SpriteByCardType(CardType);
            _spriteRenderer.enabled = true;
        }

        public void SelectCard()
        {

        }

        public void DeselectCard()
        {

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
