namespace Assets.Code.FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class State
    {
        protected StateMachine sm;
        protected State endState;

        protected Entity parent;
        protected Entity target;

        public State(StateMachine nSM, Entity nParent)
        {
            sm = nSM;
            parent = nParent;
        }
        
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void Transition() { if (endState != null) sm.ChangeState(endState); }
    }
}