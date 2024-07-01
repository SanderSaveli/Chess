using System.Collections.Generic;
using UnityEngine;

namespace OFG.Chess
{
    public sealed class Deck : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public IReadOnlyList<CardType> Cards => _cards;

        private readonly List<CardType> _cards = new();

        public bool IsEmpty() => _cards.Count == 0;

        public void Init(IEnumerable<CardType> cards)
        {
            _cards.Clear();
            _cards.AddRange(cards);
            UpdateView();
        }

        public bool TryGetCard(out CardType cardType)
        {
            if (_cards.Count != 0)
            {
                cardType = _cards[0];
                _cards.RemoveAt(0);
                UpdateView();
                return true;
            }
            cardType = CardType.None;
            return false;
        }

        private void UpdateView()
        {
            if (_cards.Count == 0)
            {
                _spriteRenderer.enabled = false;
            }
            else
            {
                _spriteRenderer.enabled = true;
            }
        }
    }
}
