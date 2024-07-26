using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    [Serializable]
    public class FigureSpritePair
    {
        [SerializeField] private FigureType _figure;
        [SerializeField] private Sprite _sprite;

        public FigureType Tool { get => _figure; }
        public Sprite Sprite { get => _sprite; }
    }
    public class UIFigureView : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Image _figureImage;

        [Header(H.Params)]
        [SerializeField] private Color _blackFigureColor = Color.black;
        [SerializeField] private Color _whiteFigureColor = Color.white;
        [SerializeField] private List<FigureSpritePair> _figurePairsList;

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
                _figurePair.Add(pair.Tool, pair.Sprite);
            }
        }
    }
}
