using UnityEngine;

namespace IUP.Toolkit
{
    public abstract class TweenBase<T>
    {
        public TweenBase(LerpFunc<T> lerpFunc) => _lerpFunc = lerpFunc;

        [field: SerializeField] public T A { get; set; }
        [field: SerializeField] public T B { get; set; }
        [field: SerializeField] public float Duration { get; set; }
        [field: SerializeField] public AnimationCurve Curve { get; set; }

        public float Timer { get; private set; }
        public bool IsReversed { get; private set; }
        public bool IsUnreversed => !IsReversed;

        private T CurrentA => IsUnreversed ? A : B;
        private T CurrentB => IsUnreversed ? B : A;

        private readonly LerpFunc<T> _lerpFunc;

        public void Complete() => Timer = Duration;

        public T Calculate()
        {
            Timer += Time.deltaTime;
            Timer = Mathf.Min(Timer, Duration);
            float timerNormalized = Timer / Duration;
            float time = Curve.Evaluate(timerNormalized);
            return _lerpFunc(CurrentA, CurrentB, time);
        }

        public void ResetTimer() => Timer = 0.0f;

        public void Reverse()
        {
            IsReversed = !IsReversed;
            ReverseTimer();
        }

        public void MakeReversed()
        {
            if (IsUnreversed)
            {
                ReverseTimer();
            }
            IsReversed = true;
        }

        public void MakeUnreversed()
        {
            if (IsReversed)
            {
                ReverseTimer();
            }
            IsReversed = false;
        }

        private void ReverseTimer() => Timer = Duration - Timer;
    }
}
