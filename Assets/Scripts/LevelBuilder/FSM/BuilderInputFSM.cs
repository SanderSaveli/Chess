using IUP.Toolkit;
using UnityEngine;

namespace OFG.ChessPeak.LevelBuild
{
    public class BuilderInputFSM : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private ToolController _toolController;
        [SerializeField] private DeckBuilderController _deckBuilderController;

        private InputStateApplyTool _stateApplyTool;
        private InputStateBuildDeck _stateDeckBuild;

        private FSM<BuilderInputState> _fsm;

        public void SetApplyToolState() => _fsm.SetState(_stateApplyTool);

        public void SetDeckBuildState() => _fsm.SetState(_stateDeckBuild);

        private void Awake()
        {
            InitStates();
            InitFSM();
        }

        private void Update() => _fsm.Update();

        private void InitStates()
        {
            BuilderInputFSM_Context context = new(_toolController, _deckBuilderController);
            _stateApplyTool = new InputStateApplyTool(context);
            _stateDeckBuild = new InputStateBuildDeck(context);
        }

        private void InitFSM() => _fsm = new FSM<BuilderInputState>(_stateApplyTool);
    }
}
