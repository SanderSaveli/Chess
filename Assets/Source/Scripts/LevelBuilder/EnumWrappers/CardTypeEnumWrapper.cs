using UnityEngine;

namespace OFG.ChessPeak
{
    public class CardTypeEnumWrapper : MonoBehaviour
    {
        [SerializeField] private CardType _cardType;

        public CardType CardType => _cardType;
    }
}
