using UnityEngine;

namespace OFG.ChessPeak
{
    public class FieldCreator : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private LevelBuilder _levelBuilder;
        [SerializeField] private GameObject defaultLevelPrefab;

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
            _field = _levelBuilder.BuildLevel(defaultLevelPrefab);
            _levelDecore = _field.gameObject.GetComponentInChildren<LevelDecore>();
            return _field;
        }
    }
}
