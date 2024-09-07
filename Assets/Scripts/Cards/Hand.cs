using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class Hand : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private CardImages _cardImages;

        [Header(H.Params)]
        [SerializeField] private Transform _cardViewRoot;
        [SerializeField] private float _cardViewOffset;

        [Header(H.Prefabs)]
        [SerializeField] private GameObject _cardPrefab;

        private readonly List<CardType> _cards = new();
        private readonly List<CardView> _cardViews = new();

        public bool IsEmpty() => _cards.IsEmpty();
        public bool IsNonEmpty() => _cards.IsNonEmpty();

        public void AddCard(CardType cardType) => InstantiateCard(cardType);

        public void RemoveCard(CardView cardView)
        {
            int i = _cardViews.IndexOf(cardView);
            RemoveCard(i);
        }

        public void RemoveCard(int i)
        {
            CardView cardView = _cardViews[i];
            cardView.Destroy();
            _cards.RemoveAt(i);
            _cardViews.RemoveAt(i);
            UpdateCardTargetPositions();
        }

        private void UpdateCardTargetPositions()
        {
            for (int i = 0; i < _cardViews.Count; i += 1)
            {
                float xOffset = i * _cardViewOffset;
                Vector3 localPosition = new(xOffset, 0.0f, 0.0f);
                CardView cardView = _cardViews[i];
                cardView.TargetPosition = _cardViewRoot.TransformPoint(localPosition);
            }
        }

        private void InstantiateCard(CardType cardType)
        {
            float xOffset = _cards.Count * _cardViewOffset;
            Vector3 localPosition = new(xOffset, 0.0f, 0.0f);
            GameObject cardViewObject = Instantiate(
                _cardPrefab,
                _cardViewRoot);
            _cards.Add(cardType);
            CardView cardView = cardViewObject.GetComponent<CardView>();
            cardView.Init(cardType, _cardImages);
            cardView.TargetPosition = _cardViewRoot.TransformPoint(localPosition);
            _cardViews.Add(cardView);
        }
    }
}
