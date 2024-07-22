using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    [RequireComponent(typeof(Image))]
    public sealed class CardView : MonoBehaviour
    {
        [Header(H.Styles)]
        [SerializeField] private float _positionSmoothTime;
        [SerializeField] private float _scaleSmoothTime = 0.5f;
        [SerializeField] private float _scaleSelectedFactor = 1.5f;
        [SerializeField] private float _yPositionOffsetOnSelection = 1.0f;
        [SerializeField] private float _yOffsetOnDestroy = 10.0f;
        [SerializeField] private float _timeToDestroy = 1.0f;

        public CardType CardType { get; private set; }
        public Vector3 TargetPosition { get; set; }
        public Vector3 TargetScale { get; set; } = Vector3.one;

        private Image _image;
        private Vector3 _positionVelocity;
        private Vector3 _scaleVelocity;

        public void Init(CardType cardType)
        {
            CardType = cardType;
            _image.enabled = true;
        }

        public void Hover() => TargetScale = Vector3.one * _scaleSelectedFactor;

        public void Unhover() => TargetScale = Vector3.one;

        public void Select()
        {
            Vector3 newTargetPosition = TargetPosition;
            newTargetPosition.y += _yPositionOffsetOnSelection;
            TargetPosition = newTargetPosition;
            Hover();
        }

        public void Unselect()
        {
            Vector3 newTargetPosition = TargetPosition;
            newTargetPosition.y -= _yPositionOffsetOnSelection;
            TargetPosition = newTargetPosition;
            Unhover();
        }

        public void Destroy() => _ = StartCoroutine(DestroyRoutine());

        private void Awake() => InitComponentReferences();

        private void Update()
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                TargetPosition,
                ref _positionVelocity,
                _positionSmoothTime);
            transform.localScale = Vector3.SmoothDamp(
                transform.localScale,
                TargetScale,
                ref _scaleVelocity,
                _scaleSmoothTime);
        }

        private void InitComponentReferences() => _image = GetComponent<Image>();

        private IEnumerator DestroyRoutine()
        {
            float time = 0.0f;
            Vector3 newTargetPosition = TargetPosition;
            newTargetPosition.y += _yOffsetOnDestroy;
            TargetPosition = newTargetPosition;
            Color color = _image.color;
            float startAlpha = color.a;
            float finalAlpha = 0.0f;
            do
            {
                yield return null;
                time += Time.deltaTime;
                color = _image.color;
                float normalizedTime = time / _timeToDestroy;
                color.a = Mathf.Lerp(startAlpha, finalAlpha, normalizedTime);
                _image.color = color;
            }
            while (time < _timeToDestroy);
            Destroy(gameObject);
        }
    }
}
