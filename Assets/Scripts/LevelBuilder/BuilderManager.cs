using System.Linq;
using TMPro;
using UnityEngine;

namespace OFG.ChessPeak.LevelBuild
{
    public class BuilderManager : MonoBehaviour
    {
        [Header(H.Params)]
        [SerializeField] private EditorMode _mode;
        
        [Header(H.ComponentReferences)]
        [SerializeField] private ToolHandler _tollHandler;
        [SerializeField] private LevelBuilder _builder;
        [SerializeField] private FieldCreator _fieldCreator;
        [SerializeField] private BuilderInputFSM _builderInputFSM;
        [SerializeField] private DeckBuilder _deckBuilder;
        [SerializeField] private BuilderNotificationManager _notificationManager;

        [Header("Controllers")]
        [SerializeField] private ToolController _toolController;
        [SerializeField] private DeckBuilderController _deckBuilderController;

        [SerializeField] private TMP_InputField LevelName;

        private OpponentAI _opponentAI;
        private GameField _gameField;
        private LevelSaver _levelSaver;
        private void Start()
        {
            _gameField = _fieldCreator.CreateField();
            _levelSaver = new(_gameField, _deckBuilder);
            _levelSaver.TryGetLastSave(OnPositionLoad);
        }

        private void OnEnable()
        {
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadMenu>(SaveEditorState);
        }

        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadMenu>(SaveEditorState);
        }
        public void OpenCreateLevelWindow() =>
            _builderInputFSM.SetIdleState();

        public void CloseCreateLevelWindow() =>
                _builderInputFSM.SetApplyToolState();


        public void OpenDeckBuildWindow() =>
            _builderInputFSM.SetDeckBuildState();

        public void CloseDeckBuildWindow() =>
            _builderInputFSM.SetApplyToolState();
        public void SaveLevel()
        {
            if(!IsPositionCorrect())
            {
                return;
            }

            if (_mode == EditorMode.levels)
            {
                _levelSaver.SaveGameLevel(LevelName.text, ShowGameLevelSaveResult);
            }
            else
            {
                _levelSaver.SaveCustomLevel(LevelName.text, ShowCustomLevelSaveResult);
            }
        }

        private bool IsPositionCorrect()
        {
            if (!_opponentAI.ÑheckPositionPossible(out IncorrectPositionReason reason))
            {
                _notificationManager.SwowNotification(reason, ButtonType.negative);
                return false;
            }
            if(!_gameField.HasWhiteFigure()) 
            {
                _notificationManager.SwowNotification(IncorrectPositionReason.NoWhiteFigures, ButtonType.negative);
                return false;
            }
            if(_deckBuilder.CardsInHand.Count == 0)
            {
                _notificationManager.SwowNotification(IncorrectPositionReason.HandEmpty, ButtonType.negative);
                return false;
            }
            if(LevelName.text == string.Empty)
            {
                _notificationManager.SwowNotification(IncorrectPositionReason.NoLevelName, ButtonType.negative);
                return false;          
            }
            return true;
        }
        private void OnPositionLoad(LevelData data)
        {
            if(!data.Equals(default(LevelData)))
            {
                _gameField = _builder.BuildLevel(data);
                _deckBuilder.AddCardsToHand(data.CardsInHand.ToList());
                _deckBuilder.AddCardsToDeck(data.CardsInDeck.ToList());
            }
            else
            {
                _gameField = _fieldCreator.CreateField();
            }
            _opponentAI = new OpponentAI(); 
            _opponentAI.Init(_gameField);
            _levelSaver = new(_gameField, _deckBuilder);
            _tollHandler.Init(_gameField);
            _toolController.Init(_tollHandler, _gameField);
            _builderInputFSM.SetApplyToolState();
        }

        private void SaveEditorState(EventInputLoadMenu data)
        {
            _levelSaver.SaveEditorState();
        }

        private void ShowGameLevelSaveResult(bool isSucsess)
        {
            if(isSucsess)
            {
                _notificationManager.ShowNotification("New Game Level created!", ButtonType.positive);
            }
            else
            {
                _notificationManager.ShowNotification("Problem with Game Level creation!", ButtonType.negative);
            }
        }

        private void ShowCustomLevelSaveResult(bool isSucsess)
        {
            if (isSucsess)
            {
                _notificationManager.ShowNotification("New Custom Level created!", ButtonType.positive);
            }
            else
            {
                _notificationManager.ShowNotification("Problem with Custom Level creation!", ButtonType.negative);
            }
        }

        private enum EditorMode
        {
            levels,
            custom
        }
    }
}
