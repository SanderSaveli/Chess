namespace IUP.Toolkit
{
    public abstract class FSM_State
    {
        public virtual void OnEnter() { }

        public virtual void OnUpdate() { }

        public virtual void OnExit() { }
    }
}
