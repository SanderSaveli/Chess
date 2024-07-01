using System;
using System.Collections;
using UnityEngine;

namespace OFG.Chess
{
    public sealed class Card : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Header(H.Styles)]
        [SerializeField] private Sprite _spritePawnCard;
        [SerializeField] private Sprite _spriteKnightCard;
        [SerializeField] private Sprite _spriteBishopCard;
        [SerializeField] private Sprite _spriteRookCard;
        [SerializeField] private Sprite _spriteQueenCard;
        [SerializeField] private Sprite _spriteKingCard;
        [SerializeField] private float _positionSmoothTime;
        [SerializeField] private float _scaleSmoothTime = 0.5f;
        [SerializeField] private float _scaleSelectedFactor = 1.5f;
        [SerializeField] private float _yPositionOffsetOnSelection = 1.0f;
        [SerializeField] private float _yOffsetOnDestroy = 10.0f;
        [SerializeField] private float _timeToDestroy = 1.0f;

        public CardType CardType { get; private set; }
        public Vector3 TargetPosition { get; set; }
        public Vector3 TargetScale { get; set; } = Vector3.one;

        private Vector3 _positionVelocity;
        private Vector3 _scaleVelocity;

        public void HoverCard() => TargetScale = Vector3.one * _scaleSelectedFactor;

        public void UnhoverCard() => TargetScale = Vector3.one;

        public void SelectCard()
        {
            Vector3 newTargetPosition = TargetPosition;
            newTargetPosition.y += _yPositionOffsetOnSelection;
            TargetPosition = newTargetPosition;
            HoverCard();
        }

        public void UnselectCard()
        {
            Vector3 newTargetPosition = TargetPosition;
            newTargetPosition.y -= _yPositionOffsetOnSelection;
            TargetPosition = newTargetPosition;
            UnhoverCard();
        }

        public void HideCard()
        {
            CardType = CardType.None;
            _spriteRenderer.enabled = false;
        }

        public void InitCard(CardType cardType)
        {
            CardType = cardType;
            _spriteRenderer.sprite = SpriteByCardType(CardType);
            _spriteRenderer.enabled = true;
        }

        public void Destroy() => _ = StartCoroutine(DestroyRoutine());

        private IEnumerator DestroyRoutine()
        {
            float time = 0;
            Vector3 newTargetPosition = TargetPosition;
            newTargetPosition.y += _yOffsetOnDestroy;
            TargetPosition = newTargetPosition;
            Color color = _spriteRenderer.color;
            float startAlpha = color.a;
            float finalAlpha = 0.0f;
            do
            {
                yield return null;
                time += Time.deltaTime;
                color = _spriteRenderer.color;
                float normalizedTime = time / _timeToDestroy;
                color.a = Mathf.Lerp(startAlpha, finalAlpha, normalizedTime);
                _spriteRenderer.color = color;
            }
            while (time < _timeToDestroy);
            Destroy(gameObject);
        }

        private void Update()
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                TargetPosition,
                ref _positionVelocity,
                _positionSmoothTime);
            transform.localScale = Vector3.SmoothDamp(
                transform.localScale,
                TargetScale,
                ref _scaleVelocity,
                _scaleSmoothTime);
        }

        private Sprite SpriteByCardType(CardType cardType) => cardType switch
        {
            CardType.Pawn => _spritePawnCard,
            CardType.Knight => _spriteKnightCard,
            CardType.Bishop => _spriteBishopCard,
            CardType.Rook => _spriteRookCard,
            CardType.Queen => _spriteQueenCard,
            CardType.King => _spriteKingCard,
            _ => throw new ArgumentOutOfRangeException(nameof(cardType))
        };
    }
}
