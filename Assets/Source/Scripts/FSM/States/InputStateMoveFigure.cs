namespace OFG.ChessPeak
{
    public sealed class InputStateMoveFigure : InputState
    {
        public InputStateMoveFigure(InputFSM_Context context) : base(context) { }

        public override void OnUpdate()
        {
            FigureController.MoveFigureUpdate();
        }
    }
}
