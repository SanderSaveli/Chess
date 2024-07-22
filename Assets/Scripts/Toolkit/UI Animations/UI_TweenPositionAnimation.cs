using UnityEngine;

namespace IUP.Toolkit
{
    public sealed class UI_TweenPositionAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _hiddenTarget;
        [SerializeField] private float _duration;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private bool _isHiddenOnAwake;

        public bool IsHidden => _tween.IsReversed;

        private TweenVector3 _tween;

        public void Hide() => _tween.MakeReversed();

        public void Show() => _tween.MakeUnreversed();

        private void Awake()
        {
            _tween = new()
            {
                Curve = _curve,
                Duration = _duration
            };
            _tween.A = _hiddenTarget.position;
            _tween.B = transform.position;
            if (_isHiddenOnAwake)
            {
                _tween.Reverse();
            }
            _tween.Complete();
        }

        private void Update() => transform.position = _tween.Calculate();
    }
}
