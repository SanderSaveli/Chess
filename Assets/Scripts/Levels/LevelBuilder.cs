using OFG.ChessPeak.LevelBuild;
using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class LevelBuilder : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Transform _levelParent;
        [SerializeField] private FieldCreator _fieldCreator;
        [SerializeField] private FigurePlacer _figurePlacer;

        private GameField _gameField;

        public GameField BuildLevel(LevelData levelTemplate)
        {
            _gameField = _fieldCreator.CreateField();
            _fieldCreator.ChangeFieldSize(levelTemplate.fieldSize);
            _figurePlacer.ArrangeFigures(levelTemplate.Figures, _gameField);
            return _gameField;
        }
    }
}
