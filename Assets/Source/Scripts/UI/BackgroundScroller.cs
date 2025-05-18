using DG.Tweening;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class BackgroundScroller : MonoBehaviour
    {
        [SerializeField] private RectTransform background;

        [SerializeField] private float animationDuration = 0.5f;

        private Vector2 leftAnchorMin = new Vector2(0f, 0f);
        private Vector2 leftAnchorMax = new Vector2(0f, 1f);
        private Vector2 rightAnchorMin = new Vector2(1f, 0f);
        private Vector2 rightAnchorMax = new Vector2(1f, 1f);

        private Tween currentTween;

        public void ScrollLeft()
        {
            ScrollTo(leftAnchorMin, leftAnchorMax);
        }

        public void ScrollRight()
        {
            ScrollTo(rightAnchorMin, rightAnchorMax);
        }

        private void ScrollTo(Vector2 targetMin, Vector2 targetMax)
        {
            currentTween?.Kill();

            background.DOPivot(new Vector2(targetMin.x, 0.5f), animationDuration);

            currentTween = DOTween.To(() => background.anchorMin, x => background.anchorMin = x, targetMin, animationDuration)
                .OnUpdate(() => background.anchoredPosition = Vector2.zero); 

            DOTween.To(() => background.anchorMax, x => background.anchorMax = x, targetMax, animationDuration)
                .OnUpdate(() => background.anchoredPosition = Vector2.zero);
        }
    }
}
