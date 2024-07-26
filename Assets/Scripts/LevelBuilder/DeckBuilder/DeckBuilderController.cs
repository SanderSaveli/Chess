using UnityEngine;

namespace OFG.ChessPeak.LevelBuild
{
    public class DeckBuilderController : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] DeckBuilder _deckBuilder;

        [Header(H.Params)]
        [SerializeField] GameObject _deckBuildWindow;

        private bool _isAcktive;

        public void Activate()
        {
            _isAcktive = true;
            _deckBuildWindow.SetActive(_isAcktive);
        }
        public void Deactivate()
        {
            _isAcktive = false;
            _deckBuildWindow.SetActive(_isAcktive);
        }


        public void AddToHand(CardTypeEnumWrapper cardType)
        {
            AddToHand(cardType.CardType);
        }
        public void AddToHand(CardType cardType)
        {
            if (!_isAcktive)
                return;
            _deckBuilder.AddCardToHand(cardType);
        }

        public void AddToDeck(CardTypeEnumWrapper cardType)
        {
            AddToDeck(cardType.CardType);
        }

        public void AddToDeck(CardType cardType)
        {
            if (!_isAcktive)
                return;
            _deckBuilder.AddCardToDeck(cardType);
        }
    }
}
