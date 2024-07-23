using UnityEngine;

namespace OFG.ChessPeak
{
    public class FieldCreator : MonoBehaviour
    {

        [SerializeField] private LevelBuilder _levelBuilder;
        [SerializeField] private GameObject defaultLevelPrefab;
        private LevelDecore _levelDecore;
        private GameField _field;

        public void ChangeFieldSize(Vector2Int newFieldSize)
        {
            _field.ChangeFieldSize(newFieldSize);
            _levelDecore.ScaleDecoreForFieldSize(newFieldSize);
        }

        public void CreateField(Vector2IntParamWrapper fieldSize)
        {
            ChangeFieldSize(fieldSize.Vec);
        }
        private void Start()
        {
            CreateLevel();
        }

        private void CreateLevel()
        {
            _field = _levelBuilder.BuildLevel(defaultLevelPrefab);
            _levelDecore = _field.gameObject.GetComponentInChildren<LevelDecore>();
        }
    }
}
