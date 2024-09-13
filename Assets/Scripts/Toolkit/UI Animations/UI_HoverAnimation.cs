using UnityEngine;
using UnityEngine.EventSystems;

namespace IUP.Toolkit.UI
{
    [AddComponentMenu("UI Animations/Hover Animation")]
    public sealed class UI_HoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Tooltip("The ratio of the enlarged element compared to its original size.")]
        [SerializeField] private float _scaleFactor = 1.1f;

        [Tooltip("The animation speed increases, the higher the speed, the faster the animation.")]
        [SerializeField] private float _transitionSpeed = 10.0f;

        private Vector3 _originalScale = Vector3.one;
        private Vector3 _targetScale = Vector3.one;

        private void OnDisable() => ResetScale();
        
        private void Start()
        {
            InitOriginalScale();
            _targetScale = _originalScale;
        }

        private void Update() => UpdateView();

        public void OnPointerEnter(PointerEventData eventData) => _targetScale = _originalScale * _scaleFactor;

        public void OnPointerExit(PointerEventData eventData) => _targetScale = _originalScale;

        private void UpdateView()
        {
            float time = Time.deltaTime * _transitionSpeed;
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                _targetScale,
                time);
        }

        private void InitOriginalScale() => _originalScale = transform.localScale;

        private void ResetScale()
        {
            transform.localScale = _originalScale;
            _targetScale = _originalScale;
        }
    }
}
