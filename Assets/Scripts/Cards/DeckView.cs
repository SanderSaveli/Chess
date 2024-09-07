using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public sealed class DeckView : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Image _spriteRenderer;

        public IReadOnlyList<CardType> Cards => _cards;

        private readonly List<CardType> _cards = new();

        public bool IsEmpty() => _cards.IsEmpty();
        public bool IsNonEmpty() => _cards.IsNonEmpty();

        public void Init(IEnumerable<CardType> cards)
        {
            _cards.Clear();
            _cards.AddRange(cards);
            UpdateView();
        }

        public bool TryGetCard(out CardType cardType)
        {
            if (_cards.IsNonEmpty())
            {
                cardType = _cards[0];
                _cards.RemoveAt(0);
                UpdateView();
                return true;
            }
            cardType = CardType.None;
            return false;
        }

        private void UpdateView() => _spriteRenderer.enabled = _cards.IsNonEmpty();
    }
}
