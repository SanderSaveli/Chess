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
        [SerializeField] private float _delayBetwenCards = 0.3f;

        public CardType CardType { get; private set; }
        public Vector3 TargetPosition { get; set; }
        public Vector3 TargetScale { get; set; } = Vector3.one;

        private Image _image;
        private Vector3 _positionVelocity;
        private Vector3 _scaleVelocity;
        private Vector3 startPos;
        public bool isAnimate = false;

        private void OnEnable()
        {
            EventBusProvider.EventBus.RegisterCallback<EventTransitionComplete>(SetCardStartPosition);
        }

        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventTransitionComplete>(SetCardStartPosition);
        }

        private void Awake() => InitComponentReferences();

        private void Update()
        {
            if (!isAnimate)
            {
                transform.position = startPos;
            }
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

        public void Init(CardType cardType, Transform startPosition)
        {
            CardType = cardType;
            _image.enabled = true;
            _image.sprite = ThemeManager.instance.actualTheme.cardSet.deckcView;
            transform.position = startPosition.position;
            TargetPosition = startPosition.position;
            startPos = startPosition.position;
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

        public void DestroyWithAnimation() => StartCoroutine(DestroyRoutine());
        public void Destroy() =>
            Destroy(transform.parent.gameObject);

        private void SetCardStartPosition(EventTransitionComplete ctx)
        {
            float delay = 0;
            isAnimate = false;
            for (int i =0; i < transform.parent.parent.childCount; i++)
            {
                if (transform.parent.parent.GetChild(i) == transform.parent)
                {
                    break;
                }
                delay += _delayBetwenCards;
            }
            StartCoroutine(WaitAndShow(delay));
        }

        public IEnumerator WaitAndShow(float delay)
        {
            yield return new WaitForSeconds(delay);
            isAnimate = true;
            _image.sprite = ThemeManager.instance.actualTheme.cardSet.GetImageOfCard(CardType);
            transform.localScale = new Vector3(0.5f, 0.5f, 0);
            TargetScale = Vector3.one;  
            TargetPosition = transform.parent.position;
        }
        private void InitComponentReferences() => _image = GetComponent<Image>();

        private IEnumerator DestroyRoutine()
        {
            _image.raycastTarget = false;
            Vector3 newTargetPosition = TargetPosition;
            transform.parent.SetParent(transform.parent.parent.parent);
            transform.position = TargetPosition;
            newTargetPosition.y += _yOffsetOnDestroy;
            TargetPosition = newTargetPosition;

            float time = 0.0f;
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
            Destroy(transform.parent.gameObject);
        }
    }
}
