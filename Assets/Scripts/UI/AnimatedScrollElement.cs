using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class AnimatedScrollElement : ScrollElement
    {
        [SerializeField] private float _scaleFactor = 1.2f;
        [SerializeField] private float _showTime = 0.6f;
        [SerializeField] private Vector3 _selectOffset = new Vector3(50, 0,0);
        [SerializeField] private Vector3 _startOffset = new Vector3(50, 0,0);
        [SerializeField] protected TMP_Text _text;
        [SerializeField] protected Image _image;

        protected Image _backgroundImage;
        protected Vector3 _defaultScale;
        protected Color _selectedColor;
        protected Color _unselectedColor;

        protected bool _isSelected;

        protected virtual void Start()
        {
            SetAlfaToAllElement(0);
        }

        protected virtual void OnEnable()
        {
            _backgroundImage = GetComponent<Image>();
            _defaultScale = transform.localScale;
        }

        public override void Ini(int index)
        {
            base.Ini(index);
            _backgroundImage = GetComponent<Image>();
            StartCoroutine(this.Show((index+1) * 0.2f));
        }

        public IEnumerator Show(float delay)
        {
            SetAlfaToAllElement(0);
            yield return new WaitForSeconds(delay);
            transform.position -= _startOffset;
            float timer = _showTime;

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                float factor = 1 - timer / _showTime;
                SetAlfaToAllElement(factor);
                transform.position += _startOffset * (Time.deltaTime / _showTime);
                yield return null;
            }
        }
        public override void Deselect()
        {
            _isSelected = false;
            transform.position -= _selectOffset;
            transform.localScale = _defaultScale;
            _backgroundImage.color = _unselectedColor;
        }

        public override void Select()
        {
            _isSelected = true;
            transform.localScale = _defaultScale * _scaleFactor;
            Color col = _selectedColor;
            col.a = _backgroundImage.color.a;
            _backgroundImage.color = col;
            transform.position += _selectOffset;
        }

        private void SetAlfaToAllElement(float alfa)
        {
            _backgroundImage.color = SetColorAlfa(_backgroundImage.color, alfa);
            if (_image != null)
                _image.color = SetColorAlfa(_image.color, alfa);
            if(_text != null) 
                _text.color = SetColorAlfa(_text.color, alfa);
        }

        protected Color SetColorAlfa(Color color, float alfa)
        {
            color.a = alfa;
            return color;
        }
    }
}
