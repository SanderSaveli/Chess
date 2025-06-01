using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    [RequireComponent(typeof(Image))]
    public class ChangeColorRadioButtonView : RadioButtonView
    {
        [SerializeField] private Color _selectColor = Color.white;

        private Color _originalColor;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _originalColor = _image.color;
        }
        public override void DeSelect()
        {
            _image.color = _originalColor;
        }

        public override void Select()
        {
            _image.color = _selectColor;
        }
    }
}
