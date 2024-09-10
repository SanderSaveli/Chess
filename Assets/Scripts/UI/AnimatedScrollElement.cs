using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class AnimatedScrollElement : ScrollElement
    {
        [SerializeField] private float scaleFactor = 1.2f;
        [SerializeField] private float showTime = 0.6f;
        [SerializeField] private Vector3 _selectOffset = new Vector3(50, 0,0);
        [SerializeField] private Vector3 startOffset = new Vector3(50, 0,0);

        private Image _image;
        private Vector3 _defaultScale;
        private Color _selectedColor;
        private Color _unselectedColor;

        protected bool _isSelected;

        private void Start()
        {
            _image = GetComponent<Image>();
            ThemeData data = ThemeManager.instance.actualTheme;
            SetTheme(data);
        }

        private void OnEnable()
        {
            _image = GetComponent<Image>();
            _defaultScale = transform.localScale;
            EventBusProvider.EventBus.RegisterCallback<EventNewThemeSet>(SetTheme);
        }
        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventNewThemeSet>(SetTheme);
        }
        private void SetTheme(EventNewThemeSet data) => SetTheme(data.ThemeData);
        private void SetTheme(ThemeData data)
        {
            _selectedColor = data.scrollElementSelected;
            _unselectedColor = data.scrollElementUnselected;
            if(_isSelected)
            {
                _image.color = _selectedColor;
            }
            else
            {
                _image.color = _unselectedColor;
            }
        }

        public override void Ini(int index)
        {
            base.Ini(index);
            _image = GetComponent<Image>();
            StartCoroutine(this.Show((index+1) * 0.2f));
        }

        public IEnumerator Show(float Delay)
        {
            float timer = Delay;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                Color color = _image.color;
                color.a = 0;
                _image.color = color;
                yield return null;
            }
            transform.position -= startOffset;
            timer = showTime;

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                Color color = _image.color;
                float factor = 1 - timer / showTime;
                color.a = factor;
                _image.color = color;
                transform.position += startOffset * (Time.deltaTime / showTime);
                yield return null;
            }
        }
        public override void Deselect()
        {
            _isSelected = false;
            transform.position -= _selectOffset;
            transform.localScale = _defaultScale;
            _image.color = _unselectedColor;
        }

        public override void Select()
        {
            _isSelected = true;
            transform.localScale = _defaultScale * scaleFactor;
            Color col = _selectedColor;
            col.a = _image.color.a;
            _image.color = col;
            transform.position += _selectOffset;
        }
    }
}
