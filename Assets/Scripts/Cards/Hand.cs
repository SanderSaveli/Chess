using System.Collections.Generic;
using UnityEngine;

namespace OFG.Chess
{
    public sealed class Hand : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private List<Card> _cardViews;

        [Header(H.Params)]
        [SerializeField] private List<CardType> _cards;

        public IReadOnlyList<CardType> Cards => _cards;

        private void Awake()
        {
            InitHand();
        }

        private void InitHand()
        {
            for (int i = 0; i < _cardViews.Count; i += 1)
            {
                int lastCardsIndex = _cards.Count - 1;
                if (i <= lastCardsIndex)
                {
                    CardType cardType = _cards[i];
                    _cardViews[i].InitCard(cardType);
                }
                else
                {
                    _cardViews[i].HideCard();
                }
            }
        }
    }
}
