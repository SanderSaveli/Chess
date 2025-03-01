using IUP.Toolkit;

namespace OFG.ChessPeak
{
    public abstract class InputState : FSM_State
    {
        public InputState(InputFSM_Context context)
        {
            Context = context;
        }

        public InputFSM_Context Context { get; }
        public CardController CardController => Context.CardController;
        public FigureController FigureController => Context.FigureController;
    }
}
