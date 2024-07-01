using System.Collections.Generic;
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
            if (_moves.Count == 0)
            {
                _king.View.Defeat();
                return false;
            }
            Random random = new();
            int randomMoveI = random.Next(0, _moves.Count);
            Vector2Int newPosition = _moves[randomMoveI];
            Move(newPosition);
            return true;
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
