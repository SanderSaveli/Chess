using System.Collections;
using System.Collections.Generic;

namespace OFG.ChessPeak
{
    public sealed class Deck : IReadOnlyList<CardType>
    {
        public CardType this[int i] => _cards[i];

        public int Count => _cards.Count;

        private readonly List<CardType> _cards = new();

        public IEnumerator<CardType> GetEnumerator() => 
            ((IEnumerable<CardType>)_cards).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _cards.GetEnumerator();
    }
}
