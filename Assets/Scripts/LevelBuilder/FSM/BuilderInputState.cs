using IUP.Toolkit;

namespace OFG.ChessPeak.LevelBuild
{
    public class BuilderInputState : FSM_State
    {
        public BuilderInputState(BuilderInputFSM_Context context)
        {
            Context = context;
        }

        public BuilderInputFSM_Context Context { get; }
        public ToolController ToolController => Context.ToolController;
        public DeckBuilderController DeckBuilderController => Context.DeckBuilderController;
    }
}
