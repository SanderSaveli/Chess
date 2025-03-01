using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace OFG.ChessPeak
{
    [CreateAssetMenu(fileName = "Level Template", menuName = "OFG/Level Template")]
    public sealed class LevelTemplate : ScriptableObject
    {
        [Header(H.Prefabs)]
        [SerializeField] private GameObject _levelPrefab;

        [Header(H.Params)]
        [SerializeField] private List<CardType> _cardsInHand;
        [SerializeField] private List<CardType> _cardsInDeck;

        public GameObject LevelPrefab => _levelPrefab;
        public IReadOnlyList<CardType> CardsInHand
        {
            get
            {
                _readOnlyCardsInHand ??= new ReadOnlyCollection<CardType>(_cardsInHand);
                return _readOnlyCardsInHand;
            }
        }
        public IReadOnlyList<CardType> CardsInDeck
        {
            get
            {
                _readOnlyCardsInDeck ??= new ReadOnlyCollection<CardType>(_cardsInDeck);
                return _readOnlyCardsInDeck;
            }
        }

        private ReadOnlyCollection<CardType> _readOnlyCardsInHand;
        private ReadOnlyCollection<CardType> _readOnlyCardsInDeck;
    }
}
