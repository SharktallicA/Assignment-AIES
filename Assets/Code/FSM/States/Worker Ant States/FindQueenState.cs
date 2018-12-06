/*
    
    Feed Queen State

        Action:
            Instructs worker ant to find the queen once food is acquired
        
        For:
            Worker Ant
        
        Starts from:
            Finding Food state
        
        Exits into:
            Feed Queen state (if queen found), Finding Food state (if food dies), Dying state (if queen dead), Attracted state (if pheromone received)

*/

using UnityEngine;

namespace Assets.Code.FSM
{
    /// <summary>
    /// Instructs worker ant to find the queen once food is acquired
    /// </summary>
    public class FindQueenState : State
    {
        public FindQueenState(StateMachine nSM, Entity nParent) : base(nSM, nParent)
        {
            parent = nParent;
            sm = nSM;
        }

        /// <summary>
        /// Finds queen ant as target
        /// </summary>
        public override void Start()
        {
            if (sm.debug) Debug.Log(parent.transform.name + ": entered Find Queen State");
            base.Start();
            //if queen dead, kill ant
            if (Object.FindObjectsOfType<QueenAnt>().Length == 0)
            {
                endState = new DyingState(sm, parent);
                Transition();
                return;
            }
            endState = new FeedQueenState(sm, parent);
            target = Object.FindObjectsOfType<QueenAnt>()[0];
        }

        /// <summary>
        /// Transit to queen ant
        /// </summary>
        public override void Update()
        {
            base.Update();

            //if queen dead, kill ant
            if (Object.FindObjectsOfType<QueenAnt>().Length == 0)
            {
                endState = new DyingState(sm, parent);
                Transition();
                return;
            }

            //if food dies, go back to finding food
            if (parent.transform.childCount == 1)
            {
                endState = new FindingFoodState(sm, parent);
                Transition();
                return;
            } //Move towards queen
            if (parent.MoveTo(target.transform.position, ((WorkerAnt)parent).dna.sight, ((WorkerAnt)parent).dna.speed))
                Transition();
        }
    }
}