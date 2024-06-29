using UnityEngine;
using UnityEngine.EventSystems;

namespace UDK.ViewElements
{
    public class HoverScaled : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Tooltip("The ratio of the enlarged element compared to its original size.")]
        [SerializeField] private float scaleFactor = 1.1f;


        [Tooltip("The animation speed increases, the higher the speed, the faster the animation.")]
        [SerializeField] private float transitionSpeed = 10f;

        private Vector3 _originalScale;
        private Vector3 _targetScale;
        private RectTransform _rectTransform;

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originalScale = _rectTransform.localScale;
            _targetScale = _originalScale;
        }

        void Update()
        {
            _rectTransform.localScale = Vector3.Lerp(_rectTransform.localScale, _targetScale, Time.deltaTime * transitionSpeed);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _targetScale = _originalScale * scaleFactor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _targetScale = _originalScale;
        }
    }
}

