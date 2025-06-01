using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public enum SlideDirection
    {
        Left,
        Right,
        Top,
        Bottom
    }

    [RequireComponent(typeof(RectTransform))]
    public class SlideScreenAnimator : ScreenAnimator
    {
        [Header("Настройки направления")]
        public SlideDirection enterFrom = SlideDirection.Left;
        public SlideDirection exitTo = SlideDirection.Right;

        [Header("Настройки анимации")]
        public float offsetMultiplier = 1.2f; // Насколько далеко уходит (1.2 = 120% ширины/высоты экрана)

        private RectTransform _rectTransform;
        private Vector2 _initialAnchoredPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _initialAnchoredPosition = _rectTransform.anchoredPosition;
            transform.localScale = Vector2.one;
        }

        public override void AnimateShow(float duration, Action callback)
        {
            gameObject.SetActive(true);

            Vector2 fromPos = GetOffsetPosition(enterFrom);
            _rectTransform.anchoredPosition = fromPos;

            _rectTransform.DOAnchorPos(_initialAnchoredPosition, duration)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => callback?.Invoke());
        }

        public override void AnimateHide(float duration, Action callback)
        {
            Debug.Log("Animate Hide");

            Vector2 toPos = GetOffsetPosition(exitTo);

            _rectTransform.DOAnchorPos(toPos, duration)
                .SetEase(Ease.InCubic)
                .OnComplete(() => callback?.Invoke());
        }

        private Vector2 GetOffsetPosition(SlideDirection direction)
        {
            Vector2 offset = Vector2.zero;
            Vector2 canvasSize = GetCanvasSize();

            switch (direction)
            {
                case SlideDirection.Left:
                    offset = new Vector2(-canvasSize.x * offsetMultiplier, _initialAnchoredPosition.y);
                    break;
                case SlideDirection.Right:
                    offset = new Vector2(canvasSize.x * offsetMultiplier, _initialAnchoredPosition.y);
                    break;
                case SlideDirection.Top:
                    offset = new Vector2(_initialAnchoredPosition.x, canvasSize.y * offsetMultiplier);
                    break;
                case SlideDirection.Bottom:
                    offset = new Vector2(_initialAnchoredPosition.x, -canvasSize.y * offsetMultiplier);
                    break;
            }

            return offset;
        }

        private Vector2 GetCanvasSize()
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null && canvas.pixelRect != null)
            {
                return canvas.pixelRect.size;
            }
            return new Vector2(Screen.width, Screen.height); // fallback
        }
    }
}
