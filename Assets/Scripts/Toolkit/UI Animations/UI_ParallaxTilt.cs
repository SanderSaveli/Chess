using UnityEngine;
using UnityEngine.EventSystems;

namespace IUP.Toolkit.UI
{
    [AddComponentMenu("UI Animations/Parallax Tilt UI")]
    public sealed class UI_ParallaxTilt : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _depth = 30.0f;
        [SerializeField] private float _returnSpeed = 5.0f;

        private bool _isHovered;
        private Vector3 _lastHoverPosition;

        private void Update() => UpdateView();

        public void OnPointerEnter(PointerEventData eventData) => _isHovered = true;

        public void OnPointerExit(PointerEventData eventData) => _isHovered = false;

        private void UpdateView()
        {
            Vector3 screenPosition = Input.mousePosition;
            if (_isHovered)
            {
                _lastHoverPosition = screenPosition;
                Vector3 rotation = Camera.main.WorldToScreenPoint(transform.position) -
                    screenPosition;
                rotation.z = 0.0f;
                rotation.Normalize();
                rotation *= _depth;
                transform.rotation = Quaternion.Euler(-rotation.y, rotation.x, rotation.z);
            }
            else if ((_lastHoverPosition - screenPosition) != Vector3.zero)
            {
                float time = _returnSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    Quaternion.identity,
                    time);
            }
        }
    }
}
