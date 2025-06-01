namespace OFG.ChessPeak
{
    public sealed class InputStateMoveFigure : InputState
    {
        public InputStateMoveFigure(InputFSM_Context context) : base(context) { }

        public override void OnUpdate()
        {
            CardController.UnselectCardUpdate();
            FigureController.SelectFigureUpdate();
            CardController.SelectCardUpdate();
            FigureController.MoveFigureUpdate();
        }
    }
}
