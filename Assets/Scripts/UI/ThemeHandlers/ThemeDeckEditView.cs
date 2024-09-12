using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class ThemeDeckEditView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {    
        [SerializeField] private Image _mainImage;
        [SerializeField] private List<Image> _animatedImages;
        [SerializeField] private Vector3 rotationOffset = new Vector3(0, 0,-20);
        [SerializeField] private Vector3 offset = new Vector3(-20, 0,0);
        [SerializeField] private float duration;

        private float timer = 0;
        private Coroutine _coroutine;
        private Vector3 _imgDefaultPos;

        private void Start()
        {
            ThemeData data = ThemeManager.instance.actualTheme;
            SetTheme(data);
            _imgDefaultPos = _animatedImages[0].transform.position;
        }

        private void OnEnable()
        {
            EventBusProvider.EventBus.RegisterCallback<EventNewThemeSet>(SetTheme);
        }
        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventNewThemeSet>(SetTheme);
        }
        private void SetTheme(EventNewThemeSet data) => SetTheme(data.ThemeData);
        private void SetTheme(ThemeData data)
        {
            _mainImage.sprite = data.deckEditImage;
            for (int i = 0; i < _animatedImages.Count; i++)
            {
                _animatedImages[i].sprite = data.deckEditImage;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(Show());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Hide());
        }

        private IEnumerator Show()
        {
            while (timer < duration)
            {
                timer += Time.deltaTime;
                float factor = Time.deltaTime / duration;
                for(int i = 0; i < _animatedImages.Count; i++)
                {
                    Transform imgTransform = _animatedImages[i].transform;
                    imgTransform.position += offset * (i + 1) * factor;
                    imgTransform.Rotate(rotationOffset * (i + 1) * factor);
                }
                yield return null;
            }
            timer = duration;
        }
        private IEnumerator Hide()
        {
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                float factor = Time.deltaTime / duration;
                for (int i = 0; i < _animatedImages.Count; i++)
                {
                    Transform imgTransform = _animatedImages[i].transform;
                    imgTransform.position -= offset * (i + 1) * factor;
                    imgTransform.Rotate(rotationOffset * (i + 1) * factor * -1);
                }
                yield return null;
            }
            SetDefaultPositionToCards();
            timer = 0;
        }

        private void SetDefaultPositionToCards()
        {
            for (int i = 0; i < _animatedImages.Count; i++)
            {
                Transform imgTransform = _animatedImages[i].transform;
                imgTransform.position = _imgDefaultPos;
                imgTransform.rotation = Quaternion.identity;
            }
        }

    }
}
