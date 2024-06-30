﻿using IUP.Toolkit;
using UnityEngine;

namespace OFG.Chess
{
    public sealed class InputFSM : MonoBehaviour
    {
        [Header("Component References:")]
        [SerializeField] private CardController _cardController;
        [SerializeField] private FigureController _figureController;

        private InputStateIdle _stateIdle;
        private InputStateSelectCard _stateSelectCard;
        private InputStateMoveFigure _stateMoveFigure;
        private FSM<InputState> _fsm;

        public void SetIdleState() => _fsm.SetState(_stateIdle);

        public void SetSelectCardState() => _fsm.SetState(_stateSelectCard);

        public void SetMoveFigureState() => _fsm.SetState(_stateMoveFigure);

        private void Awake()
        {
            InitStates();
            InitFSM();
        }

        private void Update() => _fsm.Update();

        private void InitStates()
        {
            InputFSM_Context context = new(_cardController, _figureController);
            _stateIdle = new InputStateIdle(context);
            _stateSelectCard = new InputStateSelectCard(context);
            _stateMoveFigure = new InputStateMoveFigure(context);
        }

        private void InitFSM()
        {
            _fsm = new FSM<InputState>(_stateMoveFigure);
        }
    }
}