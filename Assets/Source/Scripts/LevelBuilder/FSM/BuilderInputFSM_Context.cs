namespace OFG.ChessPeak.LevelBuild
{
    public readonly struct BuilderInputFSM_Context
    {
        public BuilderInputFSM_Context(ToolController toolCentrollerController, DeckBuilderController deckBuilderController)
        {
            ToolController = toolCentrollerController;
            DeckBuilderController = deckBuilderController;
        }

        public readonly ToolController ToolController { get; }
        public readonly DeckBuilderController DeckBuilderController { get; }
    }
}
