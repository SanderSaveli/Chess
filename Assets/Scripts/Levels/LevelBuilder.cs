using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class LevelBuilder : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Transform _levelParent;

        private GameObject _levelObject;

        public GameField BuildLevel(LevelTemplate levelTemplate)
        {
            if (_levelObject != null)
            {
                DestroyLevel();
            }
            _levelObject = Instantiate(levelTemplate.LevelPrefab, _levelParent);
            return _levelObject.GetComponent<GameField>();
        }

        public GameField BuildLevel(GameObject levelObject)
        {
            if (_levelObject != null)
            {
                DestroyLevel();
            }
            _levelObject = Instantiate(levelObject, _levelParent);
            return _levelObject.GetComponent<GameField>();
        }

        public void DestroyLevel() => Destroy(_levelObject);
    }
}
