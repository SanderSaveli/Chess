using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UDK.ViewElements
{
    public abstract class BarView<T> : MonoBehaviour where T : IComparable<T>
    {
        [Tooltip("The image is responsible for the actual scale value.")]
        [SerializeField] protected Image valueBar;

        [Tooltip("Turns on the stripe decreasing animation mode..")]
        [SerializeField] protected bool isAnimated = false;

        // These fields are only shown if isAnimated = true
        [Tooltip("Delay before starting the decay animation in seconds.")]
        [SerializeField] protected float animationDelay = 0f;

        [Tooltip("The animation speed, the higher the speed, the faster the bar decreases.")]
        [SerializeField] protected float animationSpeed = 1.0f;

        [Tooltip("The background image is responsible for the decreasing animation.")]
        [SerializeField] protected Image depletionIndicator;

        protected readonly float tolerance = 0.001f;

        protected ClampedValue<T> value { get; private set; }
        public void SetNewValueForObservation(ClampedValue<T> value)
        {
            this.value = value;
            value.OnValueCjange += UpdateValueView;
            SetUpInitialValue();
        }

        protected abstract void SetUpInitialValue();

        protected abstract void UpdateValueView(T newValue);

        protected abstract IEnumerator DescendingAnimation();
    }
}

