using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class DeckCardSlot : ThemeImageHandler, ISlot<CardType>
    {
        [SerializeField] private CardType _cardType;
        [SerializeField] private bool _isShowWithStartType;

        private void Start()
        {
            if (_isShowWithStartType)
            {
                Fill(_cardType);
            }
        }
        public void Fill(CardType value)
        {
            _cardType = value;
            SetTheme(_themeData);
        }

        protected override void SetTheme(ThemeData data)
        {
            if(_image == null)
            {
                _image = GetComponent<Image>();
            }
            _themeData = data;
            _image.sprite = data.cardSet.GetImageOfCard(_cardType);
        }
    }
}
