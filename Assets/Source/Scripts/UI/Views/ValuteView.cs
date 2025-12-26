using TMPro;
using UnityEngine;
using Zenject;
using DG.Tweening;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class ValuteView : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _addPrefab;

        [Header(H.Params)]
        [SerializeField] private float _textFade = 1f;      
        [SerializeField] private float _textDelay = 1f;      
        [SerializeField] private float _popupDuration = 1f;     
        [SerializeField] private float _popupOffsetY = 60f;
        [SerializeField] private float _scaleDuration = 0.2f;
        [SerializeField] private float _scaleAmount = 1.3f;

        private SignalBus _signalBus;
        private int _currentValute;
        private IValuteManager _valuteManager;

        [Inject]
        public void Construct(SignalBus signalBus, IValuteManager valuteManager)
        {
            _signalBus = signalBus;
            _valuteManager = valuteManager;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<SignalValuteChange>(HandleAddValute);
            _text.text = _valuteManager.CurrentValute.ToString();
            Debug.Log(_valuteManager.CurrentValute.ToString());
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<SignalValuteChange>(HandleAddValute);
        }

        private void HandleAddValute(SignalValuteChange ctx)
        {
            if (ctx.ValuteAdd == 0) return;

            TMP_Text addText = Instantiate(_addPrefab, _text.transform.parent);
            addText.text = (ctx.ValuteAdd > 0 ? "+" : "-") + ctx.ValuteAdd;

            RectTransform baseRect = _text.rectTransform;
            RectTransform addRect = addText.rectTransform;

            addRect.position = baseRect.position - new Vector3(0, _popupOffsetY, 0);
            addText.color = new Color(addText.color.r, addText.color.g, addText.color.b, 0f);

            Sequence popupSequence = DOTween.Sequence();
            popupSequence.Append(addText.DOFade(1, _textFade))
                .AppendInterval(_textDelay)
                .Append(addText.DOFade(1f, 0f))
                .Join(addRect.DOMoveY(baseRect.position.y, _popupDuration).SetEase(Ease.OutQuad))
                .Join(addText.DOFade(0f, _popupDuration).SetEase(Ease.InQuad))
                .OnComplete(() => Destroy(addText.gameObject));

            // После влета — основная анимация текста
            popupSequence.AppendCallback(() =>
            {
                Sequence textSequence = DOTween.Sequence();
                textSequence.Append(_text.transform.DOScale(_scaleAmount, _scaleDuration));
                textSequence.AppendCallback(() =>
                {
                    _text.text = ctx.Valute.ToString();
                    Debug.Log(ctx.Valute);
                    LayoutRebuilder.ForceRebuildLayoutImmediate(_text.transform.parent as RectTransform);
                });
                textSequence.Append(_text.transform.DOScale(1f, _scaleDuration));
            });
        }
    }
}
