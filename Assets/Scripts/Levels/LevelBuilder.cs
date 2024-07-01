using UnityEngine;

namespace OFG.Chess
{
    public sealed class LevelBuilder : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Transform _levelParent;

        private GameObject _levelObject;

        public GameField BuildLevel(LevelTemplate levelTemplate)
        {
            _levelObject = Instantiate(levelTemplate.LevelPrefab, _levelParent);
            return _levelObject.GetComponent<GameField>();
        }

        public void DestroyLevel() => Destroy(_levelObject);
    }
}
