using CustomText;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    [Serializable]
    public class FigureSpritePair
    {
        [SerializeField] private FigureType _figure;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _titleKey;

        public FigureType CardType { get => _figure; }
        public Sprite Sprite { get => _sprite; }
        public string StringKey { get => _titleKey; }
    }
    public class UIFigureView : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private Image _figureImage;
        [SerializeField] private TextByTableKey _figureTitle;

        [Header(H.Params)]
        [SerializeField] private Color _blackFigureColor = Color.black;
        [SerializeField] private Color _whiteFigureColor = Color.white;
        [SerializeField] private List<FigureSpritePair> _figurePairsList;

        public Action<UIFigureView> OnDestroyInput;

        public FigureType CurentFigure { get; private set; }

        private Dictionary<FigureType, Sprite> _figurePair;

        private void Awake()
        {
            CreateFigureDictionary();
        }
        public void ChangeViewImage(FigureType cardType)
        {
            _figureImage.sprite = _figurePair[cardType];
            CurentFigure = cardType;
            _figureTitle.SetText(_figurePairsList.FirstOrDefault(t=> t.CardType == cardType).StringKey);
        }

        public void ChangeViewImage(FigureType cardType, FigureColor color)
        {
            _figureImage.sprite = _figurePair[cardType];
            if (color == FigureColor.White)
                _figureImage.color = _whiteFigureColor;
            else
                _figureImage.color = _blackFigureColor;
        }

        private void CreateFigureDictionary()
        {
            _figurePair = new();
            foreach (var pair in _figurePairsList)
            {
                _figurePair.Add(pair.CardType, pair.Sprite);
            }
        }

        public void Destroy()
        {
            OnDestroyInput?.Invoke(this);
        }
    }
}
