using System.Collections.Generic;
using UnityEngine;

namespace OFG.Chess
{
    public sealed class FigureController : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private GameField _gameField;
        [SerializeField] private PointerController _pointerController;
        [SerializeField] private SelectionViewController _selectionViewController;

        private Vector2Int _cursorPosition;
        private Figure _selectedFigure;
        private Vector2Int _selectedFigurePosition;

        private List<Vector2Int> _moves = new();
        private Dictionary<Vector2Int, SelectionType> _selections = new();

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnLeftMouseButtonDown();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnLeftMouseButtonUp();
            }
            else if (Input.GetMouseButton(0))
            {
                OnLeftMouseButton();
            }
            else
            {
                IdleUpdate();
            }
        }

        private void IdleUpdate()
        {
            if (_pointerController.TryGetHoveredFigure(out Figure _, out Vector2Int position2))
            {
                UpdateCursorSelection(position2);
            }
            else if (_pointerController.TryGetHoveredCell(out CellBase _, out position2))
            {
                UpdateCursorSelection(position2);
            }
            else
            {
                HideCursorSelection();
            }
        }

        private void OnLeftMouseButtonDown()
        {
            if (_pointerController.TryGetHoveredFigure(out Figure figure, out Vector2Int position2))
            {
                _selectedFigurePosition = position2;
                _selectedFigure = figure;
                _moves.Clear();
                FigureMoves.GetMoves(ref _moves, position2, _gameField, figure.FigureType, figure.FigureColor);
                FigureSelections.GetSelections(ref _moves, ref _selections, _gameField, figure.FigureColor);
                SelectOptions();
            }
        }

        private void OnLeftMouseButton()
        {
            if (_pointerController.TryGetHoveredCell(out CellBase _, out Vector2Int position2))
            {
                if (_moves.Contains(position2) || position2 == _cursorPosition)
                {
                    UpdateCursorSelection(position2);
                }
                else
                {
                    HideCursorSelection();
                }
            }
        }

        private void OnLeftMouseButtonUp()
        {
            if (_moves.Contains(_cursorPosition))
            {
                MoveSelectedFigure(_cursorPosition);
            }
            _selectedFigurePosition = -Vector2Int.one;
            _selectedFigure = null;
            DeselectOptions();
            _selections.Clear();
        }

        private void UpdateCursorSelection(Vector2Int position2)
        {
            HideCursorSelection();
            if (position2 != -Vector2.one)
            {
                _cursorPosition = position2;
                _selectionViewController.SetSelection(position2, SelectionType.Cursor);
            }
        }

        private void HideCursorSelection()
        {
            if (_selections.TryGetValue(_cursorPosition, out SelectionType selectionType))
            {
                _selectionViewController.SetSelection(_cursorPosition, selectionType);
            }
            else
            {
                _selectionViewController.SetSelection(_cursorPosition, SelectionType.None);
            }
        }

        private void DeselectOptions()
        {
            foreach (Vector2Int position in _selections.Keys)
            {
                _selectionViewController.ResetSelection(position);
            }
        }

        private void SelectOptions()
        {
            foreach ((Vector2Int position, SelectionType selectionType) in _selections)
            {
                _selectionViewController.SetSelection(position, selectionType);
            }
        }

        private void MoveSelectedFigure(Vector2Int newPosition2)
        {
            _gameField.Figures[_selectedFigurePosition] = null;
            _gameField.Figures[newPosition2] = _selectedFigure;
            _selectedFigure.transform.position = _gameField.Position2ToWorld(newPosition2);
        }
    }
}
