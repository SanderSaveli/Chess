using System;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak.LevelBuild
{
    public class DeckBuilder : MonoBehaviour
    {
        private List<CardType> _cardsInHand = new();
        private List<CardType> _cardsInDeck = new();

        public IReadOnlyList<CardType> CardsInHand => _cardsInHand;
        public IReadOnlyList<CardType> CardsInDeck => _cardsInDeck;

        public Action<CardType> OnCardsInHandAdd;
        /// <summary>
        /// param - index of deleted element
        /// </summary>
        public Action<int> OnCardsInHandRemove;

        public Action<CardType> OnCardsInDeckAdd;
        /// <summary>
        /// param - index of deleted element
        /// </summary>
        public Action<int> OnCardsInDeckRemove;

        public void AddCardToHand(CardType card)
        {
            _cardsInHand.Add(card);
            OnCardsInHandAdd?.Invoke(card);
        }

        public void AddCardToDeck(CardType card)
        {
            _cardsInDeck.Add(card);
            OnCardsInDeckAdd?.Invoke(card);
        }

        public void RemoveCardFromHand(int cardIndex)
        {
            _cardsInHand.RemoveAt(cardIndex);
            OnCardsInHandRemove?.Invoke(cardIndex);
        }

        public void RemoveCardFromDeck(int cardIndex)
        {
            _cardsInDeck.RemoveAt(cardIndex);
            OnCardsInDeckRemove?.Invoke(cardIndex);
        }
    }
}
