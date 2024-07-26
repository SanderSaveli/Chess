using System;
using UnityEngine;

namespace OFG.ChessPeak.LevelBuild
{
    public class DeckBuilderView : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private DeckBuilder _deckBuilder;

        [Header(H.Prefabs)]
        [SerializeField] private GameObject _uiFigureView;
        [SerializeField] private Transform _handViewsParent;
        [SerializeField] private Transform _deckViewsParent;

        private void OnEnable()
        {
            _deckBuilder.OnCardsInDeckAdd += AddDeckView;
            _deckBuilder.OnCardsInDeckRemove += RemoveDeckView;
            _deckBuilder.OnCardsInHandAdd += AddHandView;
            _deckBuilder.OnCardsInHandRemove += RemoveHandView;
        }

        private void OnDisable()
        {
            _deckBuilder.OnCardsInDeckAdd -= AddDeckView;
            _deckBuilder.OnCardsInDeckRemove -= RemoveDeckView;
            _deckBuilder.OnCardsInHandAdd -= AddHandView;
            _deckBuilder.OnCardsInHandRemove -= RemoveHandView;
        }

        private void RemoveHandView(int index) 
        {
            UIFigureView[] views = _handViewsParent.GetComponentsInChildren<UIFigureView>();
            Destroy(views[index].gameObject);
        }
        private void AddHandView(CardType card)
        {
            UIFigureView view = Instantiate(_uiFigureView, _handViewsParent).GetComponent<UIFigureView>();
            view.ChangeViewImage(ConvertToFigure(card));
            view.transform.SetAsLastSibling();
        }
        private void RemoveDeckView(int index)
        {
            UIFigureView[] views = _deckViewsParent.GetComponentsInChildren<UIFigureView>();
            Destroy(views[index].gameObject);
        }
        private void AddDeckView(CardType card)
        {
            UIFigureView view = Instantiate(_uiFigureView, _deckViewsParent).GetComponent<UIFigureView>();
            view.ChangeViewImage(ConvertToFigure(card));
            view.transform.SetAsLastSibling();
        }

        private FigureType ConvertToFigure(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.Pawn: return FigureType.Pawn;
                case CardType.Queen : return FigureType.Queen;
                case CardType.King: return FigureType.King;
                case CardType.Knight: return FigureType.Knight;
                case CardType.Bishop : return FigureType.Bishop;
                case CardType.Rook: return FigureType.Rook;
                default:
                    throw new Exception("There is no key for CardType " + cardType);
            }
        }
    }
}
