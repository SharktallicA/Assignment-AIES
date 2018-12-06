/*
    
    Feeding State

        Action:
            Instructs queen ant to receive food until it is feed enough to mate
        
        For:
            Queen Ant
        
        Starts from:
            Entry point, Mating state
        
        Exits into:
            Mating state (if feed enough), Dying state (if hunger threshold is reached)

*/

using UnityEngine;

namespace Assets.Code.FSM
{
    /// <summary>
    /// Instructs queen ant to receive food until it is feed enough to mate
    /// </summary>
    public class FeedingState : State
    {

        public FeedingState(StateMachine nSM, Entity nParent) : base(nSM, nParent)
        {
            parent = nParent;
            sm = nSM;
        }

        public override void Start()
        {
            if (sm.debug) Debug.Log(parent.transform.name + ": entered Feeding State");
            base.Start();
            endState = new MatingState(sm, parent);
        }

        /// <summary>
        /// Triggers next state once queen is feed
        /// </summary>
        public override void Update()
        {
            base.Update();

            //Check if queen has starved
            if (((QueenAnt)parent).hunger <= 0)
            {
                Debug.Log(((QueenAnt)parent).hunger);
                endState = new DyingState(sm, parent);
                Transition();
            }

            //Check if queen is feed
            if (((QueenAnt)parent).feed >= ((QueenAnt)parent).feedingThreshold)
            {
                ((QueenAnt)parent).feed -= ((QueenAnt)parent).feedingThreshold;
                ((QueenAnt)parent).feedingThreshold++;
                Transition();
            }
        }
    }
}