using System;
using System.Collections.Generic;
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

        private List<UIFigureView> _deckViews = new();
        private List<UIFigureView> _handViews = new();

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
            Destroy(_handViews[index].gameObject);
            _handViews.RemoveAt(index);
        }
        private void AddHandView(CardType card)
        {
            UIFigureView view = Instantiate(_uiFigureView, _handViewsParent).GetComponent<UIFigureView>();
            view.ChangeViewImage(ConvertToFigure(card));
            view.transform.SetAsLastSibling();
            _handViews.Add(view);
            view.OnDestroyInput += DestroyHandView;
        }
        private void RemoveDeckView(int index)
        {
            Destroy(_deckViews[index].gameObject);
            _deckViews.RemoveAt(index);
        }
        private void AddDeckView(CardType card)
        {
            UIFigureView view = Instantiate(_uiFigureView, _deckViewsParent).GetComponent<UIFigureView>();
            view.ChangeViewImage(ConvertToFigure(card));
            view.transform.SetAsLastSibling();
            _deckViews.Add(view);
            view.OnDestroyInput += DestroyDeckView;
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

        private void DestroyHandView(UIFigureView view)
        {
            int index = _handViews.FindIndex(v => v == view);
            if (index != -1)
            {
                _deckBuilder.RemoveCardFromHand(index);
            }
        }

        private void DestroyDeckView(UIFigureView view)
        {
            int index = _deckViews.FindIndex(v => v == view);
            if (index != -1)
            {
                _deckBuilder.RemoveCardFromDeck(index);
            }
        }
    }
}
