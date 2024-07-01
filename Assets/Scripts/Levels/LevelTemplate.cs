using System.Collections.Generic;
using UnityEngine;

namespace OFG.Chess
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
        public IReadOnlyList<CardType> CardsInHand => _cardsInHand;
        public IReadOnlyList<CardType> CardsInDeck => _cardsInDeck;
    }
}
