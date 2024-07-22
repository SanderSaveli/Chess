using System;
using IUP.Toolkit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OFG.ChessPeak.UI
{
    public sealed class IconSlider : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private AdvancedSlider _slider;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _iconButton;

        [Header(H.Styles)]
        [SerializeField] private Sprite _spriteOn;
        [SerializeField] private Sprite _spriteOff;

        [Header(H.Events)]
        [SerializeField] private UnityEvent _onSliderSwitchToVisible;
        [SerializeField] private UnityEvent _onSliderSwitchToUnvisible;

        public bool IsSliderInteractable
        {
            get => _slider.interactable;
            private set => _slider.interactable = value;
        }
        public bool IsSliderVisible => _isSliderVisible;
        public float Value { get; private set; }

        public event Action<bool, float> ValueChanged;

        private bool _isSliderVisible;
        private bool _isPressed;
        private bool _isHovered;

        public void SetStateWithoutNofications(bool isInteractable, float value)
        {
            Value = value;
            IsSliderInteractable = isInteractable;
            UpdateView();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isHovered = true;
            UpdateView();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHovered = false;
            UpdateView();
        }

        private void Awake() => SubscribeOnEvents();

        private void OnDestroy() => UnsubscribeFromEvents();

        private void Update() => UpdateIsPressedStatus();

        private void UpdateIsPressedStatus()
        {
            if (_slider.IsPressed() && !_isPressed)
            {
                _isPressed = true;
                UpdateView();
            }
            else if (!_slider.IsPressed() && _isPressed)
            {
                _isPressed = false;
                UpdateView();
            }
        }

        private void UpdateView()
        {
            bool newSliderVisible = _isHovered || _isPressed;
            if (newSliderVisible != _isSliderVisible)
            {
                _isSliderVisible = newSliderVisible;
                if (IsSliderVisible)
                {
                    _onSliderSwitchToVisible?.Invoke();
                }
                else
                {
                    _onSliderSwitchToUnvisible?.Invoke();
                }
            }
            if (IsSliderInteractable)
            {
                _slider.SetValueWithoutNotify(Value);
                _icon.sprite = _slider.IsNotMinValue() ? _spriteOn : _spriteOff;
            }
            else
            {
                _slider.SetValueWithoutNotify(0.0f);
                _icon.sprite = _spriteOff;
            }
        }

        private void OnIconClicked()
        {
            SwitchSliderStatus();
            UpdateView();
        }

        private void OnSliderValueChanged(float newValue)
        {
            Value = newValue;
            ValueChanged?.Invoke(IsSliderInteractable, newValue);
            UpdateView();
        }

        private void SwitchSliderStatus()
        {
            IsSliderInteractable = !IsSliderInteractable;
            ValueChanged?.Invoke(IsSliderInteractable, Value);
        }

        private void SubscribeOnEvents()
        {
            _iconButton.onClick.AddListener(OnIconClicked);
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void UnsubscribeFromEvents()
        {
            _iconButton.onClick.RemoveListener(OnIconClicked);
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }
}
