using System.Collections.Generic;
using IUP.Toolkit;
using UnityEngine;

namespace OFG.Chess
{
    public sealed class CardController : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private PointerController _pointerController;
        [SerializeField] private Hand _hand;
        [SerializeField] private Deck _deck;

        public Card SelectedCard { get; private set; }

        private Card _previousHoveredCard;

        public void Init(IReadOnlyList<CardType> cardsInHand, IReadOnlyList<CardType> cardsInDeck)
        {
            _deck.Init(cardsInDeck);
            foreach (CardType cardType in cardsInHand)
            {
                _hand.AddCard(cardType);
            }
        }

        public bool HasCardOnHandsOrDeck() => !_hand.IsEmpty() || !_deck.IsEmpty();

        public void SelectCardUpdate()
        {
            UnhoverPreviousCard();
            if (_pointerController.TryGetCard(out Card hoveredCard))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SelectCard(hoveredCard);
                }
                else
                {
                    HoverCard(hoveredCard);
                }
            }
        }

        public void UnselectCardUpdate()
        {
            if (_pointerController.TryGetCard(out Card hoveredCard) &&
                Input.GetMouseButtonDown(0) &&
                (hoveredCard == SelectedCard))
            {
                UnselectCard();
            }
        }

        public void AddCard(CardType cardType) => _hand.AddCard(cardType);

        public bool TryAddCardFromDeck()
        {
            if (_deck.TryGetCard(out CardType cardType))
            {
                _hand.AddCard(cardType);
                return true;
            }
            return false;
        }

        public void RemoveSelectedCard()
        {
            _hand.RemoveCard(SelectedCard);
            SelectedCard = null;
        }

        private void HoverCard(Card card)
        {
            card.HoverCard();
            _previousHoveredCard = card;
        }

        private void UnhoverPreviousCard()
        {
            if (_previousHoveredCard != null)
            {
                _previousHoveredCard.UnhoverCard();
                _previousHoveredCard = null;
            }
        }

        private void SelectCard(Card card)
        {
            card.SelectCard();
            SelectedCard = card;
            EventCardSelected context = new(SelectedCard.CardType);
            EventBus.InvokeEvent(context);
        }

        private void UnselectCard()
        {
            SelectedCard.UnselectCard();
            SelectedCard = null;
            EventBus.InvokeEvent<EventCardUnselected>();
        }
    }
}
