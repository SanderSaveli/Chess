using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class CellColorController : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private PointerController _pointerController;
        [SerializeField] private SelectionViewController _selectionController;
        [SerializeField] private FigureController _figureController;

        private readonly Dictionary<Vector2Int, SelectionType> _selections = new();
        private GameField _gameField;

        private Figure _hoveredFigure;
        private bool _isActive;

        public void Init(GameField gameField)
        {
            _gameField = gameField;
            _isActive = true;
        }

        public void Update()
        {
            if (!_isActive)
            {
                return;
            }

            if (_pointerController.TryGetHoveredFigure(out Figure hoveredFigure, out Vector2Int position2))
            {
                if (_hoveredFigure != hoveredFigure && hoveredFigure != _figureController.SelectedFigure)
                {
                    UpdateAvailableFigurePositions(hoveredFigure, position2);
                    _hoveredFigure = hoveredFigure;
                }
            }
            else
            {
                _hoveredFigure = null;
                foreach ((Vector2Int position, SelectionType selectionType) in _selections)
                {
                    _selectionController.SetSelection(position, SelectionType.None);
                }
                _selections.Clear();
            }
        }

        private void UpdateAvailableFigurePositions(Figure figure, Vector2Int figurePos)
        {
            List<Vector2Int> moves = new List<Vector2Int>();
            _selections.Clear();
            _selectionController.ResetAllSelections();
            FigureMoves.GetMoves(moves, figurePos, _gameField, figure.FigureType, figure.FigureColor);
            FigureSelections.GetSelections(moves, _selections, _gameField, figure.FigureColor);

            foreach ((Vector2Int position, SelectionType selectionType) in _selections)
            {
                _selectionController.SetSelection(position, SelectionType.CanMoveDeactivated);
            }
        }
    }
}
