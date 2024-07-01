using System.Collections.Generic;
using IUP.Toolkit;
using UnityEngine;

namespace OFG.Chess
{
    public sealed class FigureController : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private PointerController _pointerController;
        [SerializeField] private SelectionViewController _selectionController;

        public HashSet<Vector2Int> AvailableFigurePositions => _availableFigurePositions;

        private GameField _gameField;
        private Vector2Int _previousCursorPosition;
        private Figure _selectedFigure;
        private Vector2Int _selectedFigurePosition;

        private readonly HashSet<Vector2Int> _availableFigurePositions = new();
        private List<Vector2Int> _moves = new();
        private Dictionary<Vector2Int, SelectionType> _selections = new();

        public void Init(GameField gameField) => _gameField = gameField;

        public void SelectFigureUpdate()
        {
            ResetPreviousHoveredPosition();
            if (_pointerController.TryGetHoveredFigure(out Figure hoveredFigure, out Vector2Int position2) &&
                hoveredFigure.IsWhite &&
                AvailableFigurePositions.Contains(position2))
            {
                SetCursorSelection(position2);
                if (Input.GetMouseButtonDown(0))
                {
                    SelectFigure(hoveredFigure, position2);
                    UnsetSelectedCard();
                }
            }
        }

        public void MoveFigureUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                ResetPreviousHoveredPosition();
                if (_pointerController.TryGetHoveredCell(out _, out Vector2Int position2) &&
                    (_moves.Contains(position2) || (position2 == _selectedFigurePosition)))
                {
                    SetCursorSelection(position2);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (_pointerController.TryGetHoveredCell(out _, out Vector2Int position2) &&
                    _moves.Contains(position2))
                {
                    MoveSelectedFigure();
                }
                else
                {
                    UnselectFigure();
                }
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
                    Vector2Int position2 = _gameField.Figures.CalculatePosition2(i);
                    _availableFigurePositions.Add(position2);
                }
            }
        }

        private void SelectFigure(Figure figure, Vector2Int position2)
        {
            _selectedFigurePosition = position2;
            _selectedFigure = figure;
            FigureMoves.GetMoves(ref _moves, position2, _gameField, figure.FigureType, figure.FigureColor);
            FigureSelections.GetSelections(ref _moves, ref _selections, _gameField, figure.FigureColor);
            _selections.Add(_selectedFigurePosition, SelectionType.CanMove);
            SelectOptions();
            _selectedFigure.View.Up();
            EventBus.InvokeEvent<EventFigureSelected>();
        }

        private void UnselectFigure()
        {
            _selectedFigure.View.Down();
            _selectedFigure = null;
            _selectedFigurePosition = -Vector2Int.one;
            _moves.Clear();
            _selections.Clear();
            _selectionController.ResetAllSelections();
            EventBus.InvokeEvent<EventFigureUnselected>();
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
                EventBus.InvokeEvent<EventBlackKingDefeated>();
            }
            else
            {
                _gameField.Figures[_previousCursorPosition] = _selectedFigure;
                EventBus.InvokeEvent<EventFigureMoved>();
            }
        }
    }
}
