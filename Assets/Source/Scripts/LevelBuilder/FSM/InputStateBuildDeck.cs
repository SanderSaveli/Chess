namespace OFG.ChessPeak.LevelBuild
{
    public class InputStateBuildDeck : BuilderInputState
    {
        public InputStateBuildDeck(BuilderInputFSM_Context context) : base(context)
        { }

        public override void OnEnter()
        {
            DeckBuilderController.Activate();
        }

        public override void OnExit()
        {
            DeckBuilderController.Deactivate();
        }
    }
}
