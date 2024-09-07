using IUP.Toolkit;
using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class GameManager : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private InputFSM _fsm;
        [SerializeField] private PointerController _pointerController;
        [SerializeField] private FigureController _figureController;
        [SerializeField] private CardController _cardController;
        [SerializeField] private SelectionViewController _selectionController;
        [SerializeField] private LevelBuilder _levelBuilder;
        [SerializeField] private OpponentAI _opponentAI;

        private GameField _gameField;
        private EventRegistrar _eventRegistrar;

        private void Awake() => SubscribeOnEvents();

        private void Start()
        {
            _fsm.SetSelectCardState();
        }

        private void OnDestroy() => UnsubscribeFromEvents();

        private void SubscribeOnEvents() =>
            _eventRegistrar = new EventRegistrar(EventBusProvider.EventBus)
                .RegisterCallback<EventLoadLevelComplete>(OnLoadLevelComplete)
                .RegisterCallback<EventCardSelected>(OnCardSelected)
                .RegisterCallback<EventCardUnselected>(OnCardUnselected)
                .RegisterCallback<EventFigureSelected>(OnFigureSelected)
                .RegisterCallback<EventFigureUnselected>(OnFigureUnselected)
                .RegisterCallback<EventFigureMoved>(OnFigureMoved)
                .RegisterCallback<EventBlackKingDefeated>(OnBlackKingDefeated);

        private void UnsubscribeFromEvents() => _eventRegistrar.UnregisterAll();

        private void OnLoadLevelComplete(EventLoadLevelComplete context)
        {
            _gameField = _levelBuilder.BuildLevel(context.LoadedLevelTemplate);
            _cardController.Init(
                context.LoadedLevelTemplate.CardsInHand,
                context.LoadedLevelTemplate.CardsInDeck);
            _pointerController.Init(_gameField);
            _figureController.Init(_gameField);
            _selectionController.Init(_gameField);
            _opponentAI.Init(_gameField);
        }

        private void OnCardSelected(EventCardSelected context)
        {
            _fsm.SetSelectFigureState();
            _figureController.SetSelectedCard(context.CardType);
        }

        private void OnCardUnselected(EventCardUnselected context)
        {
            _fsm.SetSelectCardState();
            _figureController.UnsetSelectedCard();
        }

        private void OnFigureSelected(EventFigureSelected context) => _fsm.SetMoveFigureState();

        private void OnFigureUnselected(EventFigureUnselected context)
        {
            _figureController.SetSelectedCard(_cardController.SelectedCard.CardType);
            _fsm.SetSelectFigureState();
        }

        private void OnFigureMoved(EventFigureMoved context)
        {
            _cardController.RemoveSelectedCard();
            if (_opponentAI.TryMakeTurn())
            {
                if (_gameField.HasWhiteFigure() && _cardController.HasCardOnHandsOrDeck())
                {
                    _fsm.SetSelectCardState();
                    _ = _cardController.TryAddCardFromDeck();

                }
                else
                {
                    _fsm.SetIdleState();
                    EventBusProvider.EventBus.InvokeEvent<EventLosing>();
                }
            }
            else
            {
                _fsm.SetIdleState();
                EventBusProvider.EventBus.InvokeEvent<EventWinning>();
            }
        }

        private void OnBlackKingDefeated(EventBlackKingDefeated context)
        {
            _fsm.SetIdleState();
            EventBusProvider.EventBus.InvokeEvent<EventWinning>();
        }
    }
}
