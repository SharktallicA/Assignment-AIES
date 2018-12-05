/*
    
    Feed Queen State

        Action:
            Instructs worker ant to give food to queen
        
        For:
            Worker Ant
        
        Starts from:
            Find Queen state
        
        Exits into:
            Finding Food state (once food given)

*/

using UnityEngine;

namespace Assets.Code.FSM
{
    /// <summary>
    /// Instructs worker ant to give food to queen
    /// </summary>
    public class FeedQueenState : State
    {
        public FeedQueenState(StateMachine nSM, Entity nParent) : base(nSM, nParent)
        {
            parent = nParent;
            sm = nSM;
        }

        /// <summary>
        /// Gets queen as target
        /// </summary>
        public override void Start()
        {
            if (sm.debug) Debug.Log(parent.transform.name + ": entered Feed Queen State");
            base.Start();
            endState = new FindingFoodState(sm, parent);
            target = Object.FindObjectsOfType<QueenAnt>()[0];
            Transition();
        }

        /// <summary>
        /// Gives food to queen
        /// </summary>
        public override void Transition()
        {
            if (parent.transform.childCount > 1)
                ((QueenAnt)target).GiveFood(parent.transform.GetChild(1).GetComponent<Fungi>());
            base.Transition();
        }
    }
}