using UnityEngine;

namespace OFG.ChessPeak.LevelBuild
{
    public class BuilderManager : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private ToolHandler _tollHandler;
        [SerializeField] private FieldCreator _fieldCreator;
        [SerializeField] private BuilderInputFSM _builderInputFSM;

        [Header("Controllers")]
        [SerializeField] private ToolController _toolController;
        [SerializeField] private DeckBuilderController _deckBuilderController;

        private GameField _gameField;
        private void Start()
        {
            _gameField = _fieldCreator.CreateField();
            _tollHandler.Init(_gameField);
            _toolController.Init(_tollHandler, _gameField);
            _builderInputFSM.SetApplyToolState();
        }

        public void OpenDeckBuildWindow() =>
            _builderInputFSM.SetDeckBuildState();

        public void CloseDeckBuildWindow() =>
            _builderInputFSM.SetApplyToolState();
    }
}
