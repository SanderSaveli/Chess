using System;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    [Serializable]
    public struct LevelData
    {
        public Vector2Int fieldSize => new Vector2Int(FieldWidth, FieldHeight);
        public int FieldWidth;
        public int FieldHeight;

        public List<FigureData> Figures;

        public List<CellData> Cells;

        public CardType[] CardsInHand;
        public CardType[] CardsInDeck;
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
