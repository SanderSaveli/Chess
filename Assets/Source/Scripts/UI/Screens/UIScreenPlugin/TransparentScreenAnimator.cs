using DG.Tweening;
using System;
using UnityEngine;

namespace OFG.ChessPeak
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TransparentScreenAnimator : ScreenAnimator
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Start()
        {
            transform.localScale = Vector3.one;
        }

        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public override void AnimateHide(float duration, Action callback)
        {
            Debug.Log("Animate Hide");
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
            Debug.Log("Animate Show");
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
