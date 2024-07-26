using UnityEngine;

namespace OFG.ChessPeak.LevelBuild
{
    public class ToolController : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private PointerController _pointerController;
        [SerializeField] private LevelBuilderFieldHilighter _fieldHilighter;

        private ToolHandler _tollHandler;
        private GameField _gameField;

        public void Init(ToolHandler toolHandler, GameField gameField)
        {
            _gameField = gameField;
            _tollHandler = toolHandler;
            _pointerController.Init(_gameField);
            _fieldHilighter.Init(_gameField);
        }
        public void OnUpdate()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (_pointerController.TryGetHoveredCell(out _, out Vector2Int position2))
                {
                    _tollHandler.ApplyTool(position2);
                }
            }
            _fieldHilighter.CheckForCursorPaint();
        }
    }
}
