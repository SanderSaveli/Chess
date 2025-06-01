using System;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    [CreateAssetMenu(fileName = "new Figure Set", menuName = "Themes/Figure Set")]
    public class FigureSet : ScriptableObject
    {
        [SerializeField]
        List<FigureTypePair> figures = new List<FigureTypePair> {
            new FigureTypePair(FigureType.King, FigureColor.Black),
            new FigureTypePair(FigureType.King, FigureColor.White),
            new FigureTypePair(FigureType.Queen, FigureColor.White),
            new FigureTypePair(FigureType.Rook, FigureColor.White),
            new FigureTypePair(FigureType.Bishop, FigureColor.White),
            new FigureTypePair(FigureType.Knight, FigureColor.White),
            new FigureTypePair(FigureType.Pawn, FigureColor.White),
        };
        public GameObject GetFigurePrefab(FigureType cardType, FigureColor color)
        {
            foreach (FigureTypePair pair in figures)
            {
                if (pair.type == cardType && pair.color == color)
                    return pair.figure;
            }
            return null;
        }
    }

    [Serializable]
    public class FigureTypePair
    {
        public FigureTypePair()
        { }
        public FigureTypePair(FigureType type, FigureColor color)
        {
            this.type = type;
            this.color = color;
        }
        public FigureType type;
        public FigureColor color;
        public GameObject figure;
    }
}
