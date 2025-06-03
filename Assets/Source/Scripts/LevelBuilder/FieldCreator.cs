using UnityEngine;
using Zenject;

namespace OFG.ChessPeak.LevelBuild
{
    public class FieldCreator : MonoBehaviour
    {

        [Header(H.Prefabs)]
        [SerializeField] private GameObject defaultLevel;
        [SerializeField] private Transform _levelParent;

        private GameObject _levelObject;
        private LevelDecore _levelDecore;
        private GameField _field;
        private DiContainer _diContainer;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void ChangeFieldSize(Vector2Int newFieldSize)
        {
            _field.ChangeFieldSize(newFieldSize);
            _levelDecore.ScaleDecoreForFieldSize(newFieldSize);
        }
        
        public void ChangeFieldSize(Vector2IntParamWrapper fieldSize)
        {
            ChangeFieldSize(fieldSize.Vec);
        }

        public GameField CreateField()
        {
            if(_levelObject != null)
            {
                DestroyLevel();
            }
            _levelObject = _diContainer.InstantiatePrefab(defaultLevel, _levelParent);
            _field = _levelObject.GetComponent<GameField>();
            _levelDecore = _field.gameObject.GetComponentInChildren<LevelDecore>();
            return _field;
        }

        public void DestroyLevel() => DestroyImmediate(_levelObject);
    }
}
