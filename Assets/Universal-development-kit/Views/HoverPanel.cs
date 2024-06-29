using UnityEngine;
using UnityEngine.EventSystems;


namespace UDK.ViewElements
{
    public class HoverPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Tooltip("The height to which the element will rise when hovered.")]
        [SerializeField] private float hoverHeight = 10f;

        [Tooltip("The animation speed, the higher the speed, the faster the animation.")]
        [SerializeField] private float transitionSpeed = 7f;

        protected bool isHovering = false;

        private Vector3 _originalPosition;
        private Vector3 _targetPosition;
        private RectTransform _rectTransform;

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originalPosition = _rectTransform.localPosition;
            _targetPosition = _originalPosition;
        }

        void Update()
        {
            _rectTransform.localPosition = Vector3.Lerp(_rectTransform.localPosition, _targetPosition, Time.deltaTime * transitionSpeed);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _targetPosition = _originalPosition + new Vector3(0, hoverHeight, 0);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _targetPosition = _originalPosition;
        }
    }

}
