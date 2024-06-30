namespace OFG.Chess
{
    public readonly struct InputFSM_Context
    {
        public InputFSM_Context(CardController cardController, FigureController figureController)
        {
            CardController = cardController;
            FigureController = figureController;
        }

        public readonly CardController CardController { get; }
        public readonly FigureController FigureController { get; }
    }
}
