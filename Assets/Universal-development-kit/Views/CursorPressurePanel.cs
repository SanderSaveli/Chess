using UnityEngine;
using UnityEngine.EventSystems;

namespace UDK.ViewElements
{
    public class CursorPressurePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Tooltip("Rotation angle to look at the cursor.")]
        [SerializeField] private float depth = 30f;

        [Tooltip("The speed at which an element returns to its initial position. The higher the value, the faster the return.")]
        [SerializeField] private float returnSpeed = 5f;

        private Camera _mainCamera;

        protected bool isHovering = false;
        private Vector3 _lastHoverPos;

        private void Start()
        {
            _mainCamera = Camera.main;
        }
        void Update()
        {
            Vector3 screenPosition = Input.mousePosition;

            if (isHovering)
            {
                _lastHoverPos = screenPosition;
                Vector3 rotationVec = _mainCamera.WorldToScreenPoint(transform.position) - screenPosition;
                rotationVec.z = 0;
                rotationVec = Vector3.Normalize(rotationVec) * depth;
                transform.rotation = Quaternion.Euler(-rotationVec.y, rotationVec.x, rotationVec.z);
            }
            else if (_lastHoverPos - screenPosition != Vector3.zero)
            {
                Quaternion taragetPos = Quaternion.Euler(0, 0, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, taragetPos, returnSpeed * Time.deltaTime);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovering = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovering = false;
        }
    }
}
