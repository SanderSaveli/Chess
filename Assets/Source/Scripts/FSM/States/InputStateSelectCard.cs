namespace OFG.ChessPeak
{
    public sealed class InputStateSelectCard : InputState
    {
        public InputStateSelectCard(InputFSM_Context context) : base(context) { }

        public override void OnUpdate() => CardController.SelectCardUpdate();
    }
}
