using UnityEngine;

namespace OFG.ChessPeak
{
    public class ToolController : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private PointerController _pointerController;
        [SerializeField] private ToolHandler _tollHandler;
        [SerializeField] private FieldCreator _fieldCreator;
        [SerializeField] private LevelBuilderFieldHilighter _fieldHilighter;

        private GameField _gameField;
        private void Start()
        {
            _gameField = _fieldCreator.CreateField();
            _tollHandler.Init(_gameField);
            _pointerController.Init(_gameField);
            _fieldHilighter.Init(_gameField);
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (_pointerController.TryGetHoveredCell(out _, out Vector2Int position2))
                {
                    _tollHandler.ApplyTool(position2);
                }
            }          
        }
    }
}
