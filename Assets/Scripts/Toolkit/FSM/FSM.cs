namespace IUP.Toolkit
{
    public class FSM<TState> where TState : FSM_State
    {
        public FSM(TState state)
        {
            CurrentState = state;
            CurrentState.OnEnter();
        }

        public TState CurrentState { get; private set; }

        public void SetState(TState state)
        {
            if (CurrentState != state)
            {
                CurrentState.OnExit();
                CurrentState = state;
                CurrentState.OnEnter();
            }
        }

        public void Update() => CurrentState.OnUpdate();
    }
}
