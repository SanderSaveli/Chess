using UnityEngine;

namespace OFG.Chess
{
    public sealed class Figure : MonoBehaviour
    {
        [Header(H.Params)]
        [SerializeField] private FigureColor _figureColor;
        [SerializeField] private FigureType _figureType;

        public bool IsWhite => FigureColor == FigureColor.White;
        public bool IsBlack => FigureColor == FigureColor.Black;
        public FigureColor FigureColor => _figureColor;
        public FigureType FigureType => _figureType;
    }
}
