using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = System.Random;

namespace OFG.Chess
{
    public class OpponentAI : MonoBehaviour
    {
        private GameField _gameField;

        private Figure _king;
        private Vector2Int _kingPosition;

        private List<Vector2Int> _moves = new();

        public void Init(GameField gameField)
        {
            _gameField = gameField;
            _ = TryFindBlackKing();
        }

        public bool TryMakeTurn()
        {
            FigureMoves.GetKingMoves(ref _moves, _kingPosition, _gameField, _king.FigureColor);
            List<Vector2Int> atackedCells = getAllAtackedCells();

            foreach (var cell in _moves)
            {
                if (!atackedCells.Contains(cell))
                {
                    Move(cell);
                    return true;
                }
            }

            if (atackedCells.Contains(_kingPosition))
            {
                _king.View.Defeat();
                return false;
            }
            if (_moves.Count == 0)
            {
                _king.View.Defeat();
                return false;
            }
            return false;
        }

        private List<Vector2Int> getAllAtackedCells()
        {
            HashSet<Vector2Int> atackedCells = new HashSet<Vector2Int>();
            for(int i =0; i < _gameField.Figures.Count; i++)
            {
                Figure figure = _gameField.Figures[i];
                Vector2Int pos = _gameField.Figures.CalculatePosition2(i);
                if (figure != null)
                {
                    if (figure.FigureColor == FigureColor.White)
                    {
                        List<Vector2Int> figureMoves = new List<Vector2Int>();  
                        FigureMoves.GetAtackCell(ref figureMoves, pos, _gameField, figure.FigureType, figure.FigureColor);
                        foreach (var move in figureMoves)
                        {
                            atackedCells.Add(move);
                        }
                    }
                }
            }
            return atackedCells.ToList();
        }

        private void Move(Vector2Int newPosition)
        {
            _gameField.Figures[_kingPosition] = null;
            _kingPosition = newPosition;
            Vector3 worldPosition = _gameField.Position2ToWorld(newPosition);
            _king.View.MoveTo(worldPosition);
            _moves.Clear();

            Figure defeatedFigure = _gameField.Figures[newPosition];
            if ((defeatedFigure != null) && defeatedFigure.IsWhite)
            {
                defeatedFigure.View.Defeat();
                _gameField.Figures[newPosition] = _king;
            }
            else
            {
                _gameField.Figures[newPosition] = _king;
            }
        }

        private bool TryFindBlackKing()
        {
            for (int i = 0; i < _gameField.Figures.Count; i += 1)
            {
                Figure figure = _gameField.Figures[i];
                if (_gameField.Figures[i] != null)
                {
                    if (figure.IsBlack && (figure.FigureType == FigureType.King))
                    {
                        _king = figure;
                        _kingPosition = _gameField.Figures.CalculatePosition2(i);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
