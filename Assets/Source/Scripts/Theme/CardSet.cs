using System;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    [CreateAssetMenu(fileName = "new Card Set", menuName = "Themes/Card Set")]
    public class CardSet : ScriptableObject
    {
        [SerializeField] private List<CardImagePair> _imagePairs = new List<CardImagePair> {
            new CardImagePair(CardType.King),
            new CardImagePair(CardType.Queen),
            new CardImagePair(CardType.Rook),
            new CardImagePair(CardType.Bishop),
            new CardImagePair(CardType.Knight),
            new CardImagePair(CardType.Pawn),
            new CardImagePair(CardType.None),
        };

        [SerializeField] private Sprite _deckCiew;
        public Sprite deckcView => _deckCiew;

        public Sprite GetImageOfCard(CardType cardType)
        {
            foreach (CardImagePair pair in _imagePairs)
            {
                if (pair.CardType == cardType)
                    return pair.image;
            }
            return null;
        }
    }


    [Serializable]
    public class CardImagePair
    {
        public CardImagePair() { }
        public CardImagePair(CardType type)
        {
            CardType = type;
        }
        public CardType CardType;
        public Sprite image;
    }
}


