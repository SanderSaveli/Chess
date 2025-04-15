using DG.Tweening;
using System;
using UnityEngine;

namespace OFG.ChessPeak
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TransparentScreenAnimator : ScreenAnimator
    {
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public override void AnimateHide(float duration, Action callback)
        {
            if (duration > 0)
            {
                _canvasGroup.DOFade(0, duration).OnComplete(() => { callback?.Invoke(); });
            }
            else
            {
                _canvasGroup.alpha = 0f;
                callback?.Invoke();
            }
        }

        public override void AnimateShow(float duration, Action callback)
        {
            if (duration > 0)
            {
                _canvasGroup.DOFade(1, duration).OnComplete(() => { callback?.Invoke(); });
            }
            else
            {
                _canvasGroup.alpha = 1f;
                callback?.Invoke();
            }
        }
    }
}
