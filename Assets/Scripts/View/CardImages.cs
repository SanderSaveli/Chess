using System;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class CardImages : MonoBehaviour
    {
        [SerializeField] private List<CardImagePair> imagePairs;

        public Sprite GetImageOfCard(CardType cardType)
        {
            foreach (CardImagePair pair in imagePairs)
            {
                if(pair.CardType == cardType)
                    return pair.image;
            }
            return null;
        }
    }
}
