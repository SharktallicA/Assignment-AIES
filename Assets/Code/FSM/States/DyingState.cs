/*
    
    Dying State

        Action:
            Instructs entity to destroy itself
        
        For:
            All entities
        
        Starts from:
            Any state
        
        Exits into:
            None

*/

using UnityEngine;

namespace Assets.Code.FSM
{
    /// <summary>
    /// Instructs entity to destroy itself
    /// </summary>
    public class DyingState : State
    {
        public DyingState(StateMachine nSM, Entity nParent) : base(nSM, nParent)
        {
            sm = nSM;
            parent = nParent;
            endState = null;
        }

        /// <summary>
        /// Kill entity
        /// </summary>
        public override void Start()
        {
            if (sm.debug) Debug.Log(parent.transform.name + ": entered Dying State");
            base.Start();
            Object.Destroy(parent.gameObject);
        }
    }
}