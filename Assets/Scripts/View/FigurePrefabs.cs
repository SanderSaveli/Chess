using System;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class FigurePrefabs : MonoBehaviour
    {
        [Serializable]
        public class FigureTypePair
        {
            public FigureType type;
            public FigureColor color;
            public GameObject figure;
        }
        [SerializeField] private List<FigureTypePair> figurePairs;

        public GameObject GetFigurePrefab(FigureType cardType, FigureColor color)
        {
            foreach (FigureTypePair pair in figurePairs)
            {
                if (pair.type == cardType && pair.color == color)
                    return pair.figure;
            }
            return null;
        }
    }
}
