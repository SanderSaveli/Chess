using System;
using UnityEngine;

namespace UDK
{
    public class ClampedValue<T> where T : IComparable<T>
    {
        private T _value;
        public T min { get; }
        public T max { get; }
        public T value
        {
            get => _value;
            set {
                _value = Clamp(value, min, max);
                Debug.Log(_value);
                OnValueCjange?.Invoke(_value);
            }
        }

        public Action<T> OnValueCjange; 

        public ClampedValue(T initialValue, T max, T min = default)
        {
            if (!NumericHelper.IsNumericType<T>())
            {
                throw new InvalidOperationException("T must be a numeric type");
            }
            this.min = min;
            this.max = max;
            value = initialValue;
        }

        public static implicit operator T(ClampedValue<T> clampedValue)
        {
            return clampedValue.value;
        }

        private T Clamp(T value, T min, T max)
        {
            if (value.CompareTo(min) < 0) return min;
            if (value.CompareTo(max) > 0) return max;
            return value;
        }
    }
}
