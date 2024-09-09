using TMPro;
using UnityEngine;

namespace OFG.ChessPeak.LevelBuild
{
    public class BuilderManager : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private ToolHandler _tollHandler;
        [SerializeField] private FieldCreator _fieldCreator;
        [SerializeField] private BuilderInputFSM _builderInputFSM;
        [SerializeField] private DeckBuilder _deckBuilder;

        [Header("Controllers")]
        [SerializeField] private ToolController _toolController;
        [SerializeField] private DeckBuilderController _deckBuilderController;

        [SerializeField] private TMP_Text LevelName;


        private GameField _gameField;
        private IStorageService _storageService;
        private LevelSaver _levelSaver;
        private void Start()
        {
            _gameField = _fieldCreator.CreateField();
            _storageService = new JsonToFileStorageService();
            _levelSaver = new(_gameField, _deckBuilder);
            _tollHandler.Init(_gameField);
            _toolController.Init(_tollHandler, _gameField);
            _builderInputFSM.SetApplyToolState();
        }

        public void OpenDeckBuildWindow() =>
            _builderInputFSM.SetDeckBuildState();

        public void CloseDeckBuildWindow() =>
            _builderInputFSM.SetApplyToolState();

        public void SaveLevel() =>
            _levelSaver.SaveGameLevel(LevelName.text);
    }
}
