using UnityEngine;

namespace OFG.ChessPeak
{
    public class DeckCardFiller : ItemFiller<DeckCardSlot, CardType>
    {
        [SerializeField] private AnimatedGridItemRemover _gridItemRemover;

        public void RemoveFirst()
        {
            _gridItemRemover.RemoveItemSmooth(0);
            _slots.RemoveAt(0);
        }
    }
}
