using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class Hand : MonoBehaviour
    {
        [Header(H.Params)]
        [SerializeField] private Transform _cardViewRoot;
        [SerializeField] private float _cardViewOffset;
        [SerializeField] private Transform _cardDeckView;

        [Header(H.Prefabs)]
        [SerializeField] private GameObject _cardPrefab;

        private readonly List<CardType> _cards = new();
        private readonly List<CardView> _cardViews = new();

        public bool IsEmpty() => _cards.IsEmpty();
        public bool IsNonEmpty() => _cards.IsNonEmpty();

        public void AddCardInGame(CardType cardType) => InstantiateCard(cardType, true);

        public void AddCardBegin(CardType cardType) => InstantiateCard(cardType, false);

        public void ClearHand()
        {
            for(int i = _cards.Count -1; i >= 0; i--)
            {
                RemoveCard(i, true);
            }
        }

        public void RemoveCard(CardView cardView)
        {
            int i = _cardViews.IndexOf(cardView);
            RemoveCard(i);
        }

        public void RemoveCard(int i, bool isEmideatly = false)
        {
            CardView cardView = _cardViews[i];
            if(isEmideatly)
            {
                cardView.Destroy();
            }
            else
            {
                cardView.DestroyWithAnimation();
            }
            _cards.RemoveAt(i);
            _cardViews.RemoveAt(i);
            StartCoroutine(UpdateCardTargetPositions());
        }

        private IEnumerator UpdateCardTargetPositions()
        {
            yield return null;
            for (int i = 0; i < _cardViews.Count; i += 1)
            {
                CardView cardView = _cardViews[i];
                Vector3 pos = cardView.transform.position;
                cardView.transform.position = cardView.TargetPosition;
                cardView.TargetPosition = pos;
            }
        }

        private void InstantiateCard(CardType cardType, bool isInGame)
        {
            GameObject cardViewObject = Instantiate(
                _cardPrefab,
                _cardViewRoot);
            _cards.Add(cardType);
            CardView cardView = cardViewObject.GetComponentInChildren<CardView>();
            cardView.Init(cardType, _cardDeckView);
            _cardViews.Add(cardView);
            if(isInGame)
            {
                StartCoroutine(cardView.WaitAndShow(0.01f));
            }
        }
    }
}
