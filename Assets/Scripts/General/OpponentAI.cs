using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class OpponentAI : MonoBehaviour
    {
        private GameField _gameField;

        private Figure _king;
        private Vector2Int _kingPosition;

        private readonly List<Vector2Int> _moves = new();

        public void Init(GameField gameField)
        {
            _gameField = gameField;
            _ = TryFindBlackKing();
            _moves.Clear();
        }

        public bool TryMakeTurn()
        {
            List<Vector2Int> atackedCells = GetAllAtackedCells();
            if (TryFindMove(out Vector2Int bestMove, atackedCells))
            {
                Move(bestMove);
                return true;
            }
            else
            {
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
        }

        private List<Vector2Int> GetAllAtackedCells()
        {
            HashSet<Vector2Int> atackedCells = new HashSet<Vector2Int>();
            for(int i = 0; i < _gameField.Figures.Count; i += 1)
            {
                Figure figure = _gameField.Figures[i];
                Vector2Int position2 = _gameField.Figures.ToCoordinate(i);
                if (figure != null)
                {
                    if (figure.FigureColor == FigureColor.White)
                    {
                        List<Vector2Int> moves = new();  
                        FigureAttacks.GetAtacks(moves, position2, _gameField, figure.FigureType, figure.FigureColor);
                        foreach (Vector2Int move in moves)
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
                        _kingPosition = _gameField.Figures.ToCoordinate(i);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool TryFindMove(out Vector2Int bestMove, List<Vector2Int> atackedCells)
        {
            FigureMoves.GetKingMoves(_moves, _kingPosition, _gameField, _king.FigureColor);

            bestMove = new Vector2Int(-1,-1);
            float maxCellPriority = -1;
            foreach (var cell in _moves)
            {
                if (!atackedCells.Contains(cell))
                {
                    float cellPriority = 0;
                    if (_gameField.TryGetFigure(out Figure figure, cell))
                    {
                        cellPriority += GetPriorityForFigure(figure.FigureType);
                    }
                    cellPriority += GetPriorityForDistanceFromCorner(cell);
                    if(cellPriority > maxCellPriority)
                    {
                        maxCellPriority = cellPriority;
                        bestMove = cell;
                    }
                }
            }
            if(bestMove.x == -1)
            {
                return false;
            }
            return true;
        }

        private float GetPriorityForFigure(FigureType figureType)
        {
            switch (figureType)
            {
                case FigureType.King:
                    return 10;
                case FigureType.Queen:
                    return 8;
                case FigureType.Rook:
                    return 5;
                case FigureType.Bishop:
                    return 3;
                case FigureType.Knight:
                    return 3;
                case FigureType.Pawn:
                    return 1;
                default: 
                    return 0;
            }
        }

        private float GetPriorityForDistanceFromCorner(Vector2Int cell)
        {
            float width = _gameField.width;
            float height = _gameField.height;

            Vector2 center = new Vector2(width / 2f, height / 2f);

            float maxDistance = Vector2.Distance(Vector2.zero, center);

            Vector2 cellPosition = new Vector2(cell.x, cell.y);

            float distanceToCenter = Vector2.Distance(cellPosition, center);

            float priority = 1 - (distanceToCenter / maxDistance);

            return priority;
        }

    }
}
