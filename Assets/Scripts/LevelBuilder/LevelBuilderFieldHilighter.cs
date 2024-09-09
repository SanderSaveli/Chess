using IUP.Toolkit;
using UnityEngine;

namespace OFG.ChessPeak.LevelBuild
{
    public class LevelBuilderFieldHilighter : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private SelectionViewController _selectionViewController;
        [SerializeField] private PointerController _pointerController;

        private EventRegistrar _eventRegistrar;
        private GameField _gameField;
        private Vector2Int _lastSelectedCell = Vector2Int.zero;
        private ToolTypes _currentTool;
        public void Init(GameField gameField)
        {
            _gameField = gameField;
            _selectionViewController.Init(_gameField);
        }

        private void Awake() => SubscribeOnEvents();

        private void OnDestroy() => UnsubscribeFromEvents();

        private void OnDisable() => UnsubscribeFromEvents();

        public void CheckForCursorPaint()
        {
            if (_pointerController.TryGetHoveredCell(out _, out Vector2Int pos))
            {
                if (pos == _lastSelectedCell)
                    return;
                HilightCell(pos);
                _lastSelectedCell = pos;
            }
            else
            {
                _selectionViewController.ResetSelection(_lastSelectedCell);
            }
        }

        private void ResetHilight(EventFigurePlacedInBuilder context)
        {
            _lastSelectedCell = Vector2Int.zero;
        }

        private void HilightCell(Vector2Int pos) 
        {
            _selectionViewController.ResetSelection(_lastSelectedCell);

            if (_gameField.TryGetFigure(out _, pos))
            {
                if (_currentTool == ToolTypes.DeleteFigures)
                {
                    SetPositiveSelection(pos);
                }
                else
                {
                    SetNegativeSelection(pos);
                }
            }
            else
            {
                if (_currentTool == ToolTypes.DeleteFigures)
                {
                    SetNegativeSelection(pos);
                }
                else
                {
                    SetPositiveSelection(pos);
                }
            }
        }
        private void SetPositiveSelection(Vector2Int pos)
        {
            _selectionViewController.SetSelection(pos, SelectionType.CanMove);
        }

        private void SetNegativeSelection(Vector2Int pos)
        {
            _selectionViewController.SetSelection(pos, SelectionType.Attack);
        }
        private void SubscribeOnEvents() =>
                _eventRegistrar = new EventRegistrar(EventBusProvider.EventBus)
                    .RegisterCallback<EventToolSelected>(OnNewToolSelected)
                    .RegisterCallback<EventFigurePlacedInBuilder>(ResetHilight);

        private void UnsubscribeFromEvents() => _eventRegistrar.UnregisterAll();

        private void OnNewToolSelected(EventToolSelected context)
        {
            _currentTool = context.Tool;
        }
    }
}
