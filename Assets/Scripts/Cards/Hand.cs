using System.Collections.Generic;
using UnityEngine;

namespace OFG.Chess
{
    public sealed class Hand : MonoBehaviour
    {
        [Header(H.Params)]
        [SerializeField] private Transform _cardViewRoot;
        [SerializeField] private Transform _cardStartPoint;
        [SerializeField] private float _cardViewOffset;

        [Header(H.Prefabs)]
        [SerializeField] private GameObject _cardPrefab;

        private readonly List<CardType> _cards = new();
        private readonly List<Card> _cardViews = new();

        public bool IsEmpty() => _cards.Count == 0;

        public void AddCard(CardType cardType) => InstantiateCard(cardType);

        public void RemoveCard(Card cardView)
        {
            int i = _cardViews.IndexOf(cardView);
            RemoveCard(i);
        }

        public void RemoveCard(int i)
        {
            Card cardView = _cardViews[i];
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
                Card cardView = _cardViews[i];
                cardView.TargetPosition = _cardViewRoot.TransformPoint(localPosition);
            }
        }

        private void InstantiateCard(CardType cardType)
        {
            float xOffset = _cards.Count * _cardViewOffset;
            Vector3 localPosition = new(xOffset, 0.0f, 0.0f);
            GameObject cardViewObject = Instantiate(
                _cardPrefab,
                _cardStartPoint.position,
                _cardViewRoot.rotation,
                _cardViewRoot);
            _cards.Add(cardType);
            Card cardView = cardViewObject.GetComponent<Card>();
            cardView.InitCard(cardType);
            cardView.TargetPosition = _cardViewRoot.TransformPoint(localPosition);
            _cardViews.Add(cardView);
        }
    }
}
