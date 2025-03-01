using IUP.Toolkit;

namespace OFG.ChessPeak
{
    public class ThemeShopInputState : FSM_State
    {
        public ThemeShopInputState(ThemeShopInputFSM_Context context)
        {
            Context = context;
        }

        public ThemeShopInputFSM_Context Context { get; }
    }
}
