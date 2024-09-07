using UnityEngine;

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
                Debug.Log("Level destructed");
                DestroyLevel();
            }
            _levelObject = Instantiate(defaultLevel, _levelParent);
            _field = _levelObject.GetComponent<GameField>();
            _levelDecore = _field.gameObject.GetComponentInChildren<LevelDecore>();
            return _field;
        }

        public void DestroyLevel() => DestroyImmediate(_levelObject);
    }
}
