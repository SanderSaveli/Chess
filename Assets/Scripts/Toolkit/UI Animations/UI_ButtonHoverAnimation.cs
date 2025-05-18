using CustomText;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class UI_ButtonHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector3 _hoveredRotation;
        [SerializeField] private float _animationDuration = 0.2f;
        [SerializeField] Transform _target;
        [SerializeField] private Image _image;

        [SerializeField] private Custom_ColorStyle _normalColor;
        [SerializeField] private Custom_ColorStyle _hoveredColor;

        private Vector3 _originalRotation = Vector3.one;

        private void OnDisable()
        {
            DOTween.KillAll(gameObject);
            ResetScale();
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            _target.DORotate(_hoveredRotation, _animationDuration);
            _image.DOColor(ColorSettings.Instance.GetColor(_hoveredColor), _animationDuration);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _target.DORotate(_originalRotation, _animationDuration);
            _image.DOColor(ColorSettings.Instance.GetColor(_normalColor), _animationDuration);
        }

        private void ResetScale()
        {
            _target.rotation = Quaternion.Euler(_originalRotation);
            _image.color = ColorSettings.Instance.GetColor(_normalColor);
        }
    }
}
