namespace OFG.ChessPeak
{
    public class InputStateSelectFigure : InputState
    {
        public InputStateSelectFigure(InputFSM_Context context) : base(context) { }

        public override void OnUpdate()
        {
            CardController.UnselectCardUpdate();
            FigureController.SelectFigureUpdate();
            CardController.SelectCardUpdate();
        }
    }
}
