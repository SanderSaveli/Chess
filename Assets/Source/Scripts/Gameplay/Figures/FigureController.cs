using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace OFG.ChessPeak
{
    public sealed class FigureController : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private PointerController _pointerController;
        [SerializeField] private SelectionViewController _selectionController;

        public HashSet<Vector2Int> AvailableFigurePositions => _availableFigurePositions;
        public Figure SelectedFigure => _selectedFigure;

        private GameField _gameField;
        private Vector2Int _previousCursorPosition;
        private Figure _selectedFigure;
        private Vector2Int _selectedFigurePosition;
        private Vector3 _startDragFrom;

        private readonly HashSet<Vector2Int> _availableFigurePositions = new();
        private readonly List<Vector2Int> _moves = new();
        private readonly Dictionary<Vector2Int, SelectionType> _selections = new();

        public void Init(GameField gameField) => _gameField = gameField;

        public void SelectFigureUpdate()
        {
            if (IsPointerDown())
            {
                if (_selectedFigure != null)
                {
                    if (HandleMove())
                    {
                        return;
                    }
                }

                if (_pointerController.TryGetHoveredFigure(out Figure hoveredFigure, out Vector2Int position2) &&
                hoveredFigure.IsWhite &&
                AvailableFigurePositions.Contains(position2))
                {
                    HandleSelect(position2, hoveredFigure);
                }
            }
        }

        private void HandleSelect(Vector2Int pos, Figure figure)
        {
            SetCursorSelection(pos);
            _startDragFrom = _pointerController.GetPointerPosition();
            SelectFigure(figure, pos);
            UnsetSelectedCard();
        }

        public void MoveFigureUpdate()
        {
            if (_selectedFigure!=null)
            {
                ResetPreviousHoveredPosition();
                if (_pointerController.TryGetHoveredCell(out _, out Vector2Int position2) &&
                    (_moves.Contains(position2) || (position2 == _selectedFigurePosition)))
                {
                    SetCursorSelection(position2);
                }
                SelectOptions();
            }
            else if (IsPointerUp())
            {
                if((_startDragFrom - (Vector3)_pointerController.GetPointerPosition()).magnitude > 0.2f)
                {
                    HandleMove();
                }
            }
        }

        private bool HandleMove()
        {
            if (_pointerController.TryGetHoveredCell(out _, out Vector2Int position2) &&
_moves.Contains(position2))
            {
                SetCursorSelection(position2);
                MoveSelectedFigure();
                return true;
            }
            else
            {
                UnselectFigure();
                return false;
            }
        }
        public void UnsetSelectedCard()
        {
            foreach (Vector2Int position2 in AvailableFigurePositions)
            {
                if (_selections.TryGetValue(position2, out SelectionType selectionType))
                {
                    _selectionController.SetSelection(position2, selectionType);
                }
                else
                {
                    _selectionController.ResetSelection(position2);
                }
            }
            if (_previousCursorPosition != -Vector2Int.one)
            {
                _selectionController.SetSelection(_previousCursorPosition, SelectionType.Cursor);
            }
            _availableFigurePositions.Clear();
        }

        public void SetSelectedCard(CardType cardType)
        {
            UpdateAvailableFigurePositions(cardType);
            SelectAvailablePositions();
        }

        private void SetCursorSelection(Vector2Int position2)
        {
            _selectionController.SetSelection(position2, SelectionType.Cursor);
            _previousCursorPosition = position2;
        }

        private void ResetPreviousHoveredPosition()
        {
            if (_previousCursorPosition != -Vector2Int.one)
            {
                if (_availableFigurePositions.Contains(_previousCursorPosition))
                {
                    _selectionController.SetSelection(_previousCursorPosition, SelectionType.CanMove);
                }
                else if (_selections.TryGetValue(_previousCursorPosition, out SelectionType selectionType))
                {
                    _selectionController.SetSelection(_previousCursorPosition, selectionType);
                }
                else
                {
                    _selectionController.ResetSelection(_previousCursorPosition);
                }
                _previousCursorPosition = -Vector2Int.one;
            }
        }

        private void SelectAvailablePositions()
        {
            foreach (Vector2Int positon2 in _availableFigurePositions)
            {
                _selectionController.SetSelection(positon2, SelectionType.CanMove);
            }
        }

        private void UpdateAvailableFigurePositions(CardType cardType)
        {
            FigureType figureType = cardType.ToFigureType();
            for (int i = 0; i < _gameField.Figures.Count; i += 1)
            {
                Figure figure = _gameField.Figures[i];
                if ((figure != null) && (figure.FigureType == figureType) && (figure.IsWhite))
                {
                    Vector2Int position2 = _gameField.Figures.ToCoordinate(i);
                    _availableFigurePositions.Add(position2);
                }
            }
        }

        private void SelectFigure(Figure figure, Vector2Int position2)
        {
            UnselectFigure();
            _selectedFigurePosition = position2;
            _selectedFigure = figure;
            FigureMoves.GetMoves(_moves, position2, _gameField, figure.FigureType, figure.FigureColor);
            FigureSelections.GetSelections(_moves, _selections, _gameField, figure.FigureColor);
            _selections.Add(_selectedFigurePosition, SelectionType.CanMove);
            SelectOptions();
            _selectedFigure.View.Up();
            EventBusProvider.EventBus.InvokeEvent<EventFigureSelected>();
        }

        private void UnselectFigure()
        {
            if(_selectedFigure == null )
            {
                return;
            }
            _selectedFigure.View.Down();
            _selectedFigure = null;
            _selectedFigurePosition = -Vector2Int.one;
            _moves.Clear();
            _selections.Clear();
            _selectionController.ResetAllSelections();
            EventBusProvider.EventBus.InvokeEvent<EventFigureUnselected>();
        }

        private void SelectOptions()
        {
            foreach ((Vector2Int position, SelectionType selectionType) in _selections)
            {
                _selectionController.SetSelection(position, selectionType);
            }
        }

        private void MoveSelectedFigure()
        {
            _gameField.Figures[_selectedFigurePosition] = null;
            Vector3 worldPosition = _gameField.Position2ToWorld(_previousCursorPosition);
            _selectedFigure.View.Down();
            _selectedFigure.View.MoveTo(worldPosition);
            _selectionController.ResetAllSelections();
            _moves.Clear();
            _selections.Clear();
            _availableFigurePositions.Clear();

            Figure defeatedFigure = _gameField.Figures[_previousCursorPosition];
            if ((defeatedFigure != null) && defeatedFigure.IsBlack)
            {
                defeatedFigure.View.Defeat();
                EventBusProvider.EventBus.InvokeEvent<EventBlackKingDefeated>();
            }
            else
            {
                _gameField.Figures[_previousCursorPosition] = _selectedFigure;
                EventBusProvider.EventBus.InvokeEvent<EventFigureMoved>();
            }
            _selectedFigure = null;
        }

        public bool IsPointerDown()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return Input.GetMouseButtonDown(0);
#elif UNITY_ANDROID || UNITY_IOS
    return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
#else
    return Input.GetMouseButtonDown(0);
#endif
        }

        public bool IsPointerUp()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return Input.GetMouseButtonUp(0);
#elif UNITY_ANDROID || UNITY_IOS
    return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
#else
    return Input.GetMouseButtonUp(0);
#endif
        }
    }
}
