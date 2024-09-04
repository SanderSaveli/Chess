using System;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    [Serializable]
    public struct LevelData
    {
        public int fieldWidth;
        public int fieldHeight;

        public List<FigureData> figures;

        public List<CellData> cells;

        public CardType[] cardsInHand;
        public CardType[] cardsInDeck;
    }

    [Serializable]
    public struct CellData
    {
        public CellData(Vector2Int pos, CellType type)
        {
            this.pos = pos;
            this.type = type;
        }
        public readonly Vector2Int pos;
        public readonly CellType type;
    }

    [Serializable]
    public struct FigureData
    {
        public FigureData(Vector2Int pos, FigureType type, FigureColor color)
        {
            this.pos = pos;
            this.type = type;
            this.color = color;
        }
        public readonly Vector2Int pos;
        public readonly FigureType type;
        public readonly FigureColor color;
    }
}
