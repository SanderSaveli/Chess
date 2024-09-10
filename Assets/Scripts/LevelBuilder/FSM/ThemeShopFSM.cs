using IUP.Toolkit;
using OFG.ChessPeak.LevelBuild;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class ThemeShopFSM : MonoBehaviour
    {   
        [SerializeField] private FSM<ThemeShopInputState> _fsm;
        
        private ThemeShopInputStateIdle _stateIdle;
        private ThemeShopInputStateSelectTheme _stateSelectTheme;


        public void SetIdleState() => _fsm.SetState(_stateIdle);

        public void SetSelectThemeState() => _fsm.SetState(_stateSelectTheme);

        private void Awake()
        {
            InitStates();
            InitFSM();
        }

        private void Update() => _fsm.Update();

        private void InitStates()
        {
            ThemeShopInputFSM_Context context = new();
            _stateSelectTheme = new ThemeShopInputStateSelectTheme(context);
            _stateIdle = new ThemeShopInputStateIdle(context);
        }

        private void InitFSM() => _fsm = new FSM<ThemeShopInputState>(_stateIdle);
    }
}
