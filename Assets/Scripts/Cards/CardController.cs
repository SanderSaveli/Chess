using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class CardController : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private PointerController _pointerController;
        [SerializeField] private Hand _hand;
        [SerializeField] private DeckView _deck;

        public CardView SelectedCard { get; private set; }

        private CardView _previousHoveredCard;

        public void Init(IReadOnlyList<CardType> cardsInHand, IReadOnlyList<CardType> cardsInDeck)
        {
            _deck.Init(cardsInDeck);
            _hand.ClearHand();
            foreach (CardType cardType in cardsInHand)
            {
                _hand.AddCardBegin(cardType);
            }
        }

        public bool HasCardOnHandsOrDeck() => _hand.IsNonEmpty() || _deck.IsNonEmpty();

        public void SelectCardUpdate()
        {
            UnhoverPreviousCard();
            if (_pointerController.TryGetCard(out CardView hoveredCard))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if(SelectedCard != hoveredCard)
                    {
                        UnselectCard();
                    }
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
            if (_pointerController.TryGetCard(out CardView hoveredCard) &&
                Input.GetMouseButtonDown(0) &&
                (hoveredCard == SelectedCard))
            {
                UnselectCard();
            }
        }

        public void AddCard(CardType cardType) => _hand.AddCardInGame(cardType);

        public bool TryAddCardFromDeck()
        {
            if (_deck.TryGetCard(out CardType cardType))
            {
                _hand.AddCardInGame(cardType);
                return true;
            }
            return false;
        }

        public void RemoveSelectedCard()
        {
            _hand.RemoveCard(SelectedCard);
            SelectedCard = null;
        }

        private void HoverCard(CardView card)
        {
            card.Hover();
            _previousHoveredCard = card;
        }

        private void UnhoverPreviousCard()
        {
            if (_previousHoveredCard != null)
            {
                _previousHoveredCard.Unhover();
                _previousHoveredCard = null;
            }
        }

        private void SelectCard(CardView card)
        {
            card.Select();
            SelectedCard = card;
            EventCardSelected context = new(SelectedCard.CardType);
            EventBusProvider.EventBus.InvokeEvent(context);
        }

        private void UnselectCard()
        {
            if(SelectedCard == null) 
            {
                return;
            }
            SelectedCard.Unselect();
            SelectedCard = null;
            EventBusProvider.EventBus.InvokeEvent<EventCardUnselected>();
        }
    }
}
