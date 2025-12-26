using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;
        [SerializeField] private bool _isFloorToInt;

        private void OnEnable()
        {
            _slider.value = GetCloseness(SetSliderValue());
            _slider.onValueChanged.AddListener(HandleValueChange);
            HandleValueChange(_slider.value);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(HandleValueChange);
        }

        private void HandleValueChange(float value)
        {
            Debug.Log("value " + value);
            float v = Mathf.Lerp(_minValue, _maxValue, value);

            if (_isFloorToInt)
            {
                v = Mathf.Round(v);
            }
            else
            {
                v = (float)Math.Round((double)v, 1);
            }
            _text.text = v.ToString();
            Debug.Log("v " + v);
            HandleValue(v);
        }
        protected virtual float SetSliderValue()
        {
            return _slider.value;
        }
        protected virtual void HandleValue(float value)
        {

        }
        float GetCloseness(float value)
        {
             return Mathf.InverseLerp(_minValue, _maxValue, value);
        }
    }
}
