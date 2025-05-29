using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

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
            _slider.onValueChanged.AddListener(HandleValueChange);
            HandleValueChange(_slider.value);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(HandleValueChange);
        }

        private void HandleValueChange(float value)
        {
            Debug.Log(value);
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
        }
    }
}
