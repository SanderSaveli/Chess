using IUP.Toolkit;
using UnityEngine;

namespace OFG.Chess
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

        [Header(H.Params)]
        [SerializeField] private LevelTemplate _levelTemplate;

        private GameField _gameField;

        private void Awake()
        {
            SubscribeOnEvents();
            _gameField = _levelBuilder.BuildLevel(_levelTemplate);
            _pointerController.Init(_gameField);
            _figureController.Init(_gameField);
            _selectionController.Init(_gameField);
            _cardController.Init(_levelTemplate.CardsInHand, _levelTemplate.CardsInDeck);
            _opponentAI.Init(_gameField);
        }

        private void Start() => _fsm.SetSelectCardState();

        private void OnDestroy() => UnsubscribeFromEvents();

        private void SubscribeOnEvents()
        {
            EventBus.RegisterCallback<EventCardSelected>(OnCardSelected);
            EventBus.RegisterCallback<EventCardUnselected>(OnCardUnselected);
            EventBus.RegisterCallback<EventFigureSelected>(OnFigureSelected);
            EventBus.RegisterCallback<EventFigureUnselected>(OnFigureUnselected);
            EventBus.RegisterCallback<EventFigureMoved>(OnFigureMoved);
            EventBus.RegisterCallback<EventBlackKingDefeated>(OnBlackKingDefeated);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.UnregisterCallback<EventCardSelected>(OnCardSelected);
            EventBus.UnregisterCallback<EventCardUnselected>(OnCardUnselected);
            EventBus.UnregisterCallback<EventFigureSelected>(OnFigureSelected);
            EventBus.UnregisterCallback<EventFigureUnselected>(OnFigureUnselected);
            EventBus.UnregisterCallback<EventFigureMoved>(OnFigureMoved);
            EventBus.UnregisterCallback<EventBlackKingDefeated>(OnBlackKingDefeated);
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
                if (HasWhiteFigure() && _cardController.HasCardOnHandsOrDeck())
                {
                    _fsm.SetSelectCardState();
                    _ = _cardController.TryAddCardFromDeck();

                }
                else
                {
                    _fsm.SetIdleState();
                    EventBus.InvokeEvent<EventLosing>();
                    Debug.Log("YOU LOSE");
                }
            }
            else
            {
                _fsm.SetIdleState();
                EventBus.InvokeEvent<EventWinning>();
                Debug.Log("YOU WIN");
            }
        }

        private void OnBlackKingDefeated(EventBlackKingDefeated context)
        {
            _fsm.SetIdleState();
            EventBus.InvokeEvent<EventWinning>();
            Debug.Log("YOU WIN");
        }

        private bool HasWhiteFigure()
        {
            for (int i = 0; i < _gameField.Figures.Count; i += 1)
            {
                Figure figure = _gameField.Figures[i];
                if ((figure != null) && figure.IsWhite)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
