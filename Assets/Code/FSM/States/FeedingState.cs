/*
    
    Feeding State

        Action:
            Instructs queen ant to await food
        
        For:
            Queen Ant
        
        Starts from:
            Entry, Mating State
        
        Exits into:
            Mating State

*/

using UnityEngine;

namespace Assets.Code.FSM
{
    /// <summary>
    /// Instructs queen ant to await food
    /// </summary>
    public class FeedingState : State
    {
        /// <summary>
        /// Serves as a countdown until queen starves
        /// </summary>
        float hunger = 100f;

        public FeedingState(StateMachine nSM, Entity nParent) : base(nSM, nParent)
        {
            parent = nParent;
            sm = nSM;
        }

        public override void Start()
        {
            Debug.Log(parent.transform.name + ": entered Feeding State");
            base.Start();
            endState = new MatingState(sm, parent);
        }

        /// <summary>
        /// Triggers next state once queen is feed
        /// </summary>
        public override void Update()
        {
            base.Update();
            hunger -= (1f * ((QueenAnt)parent).dna.hunger) * Time.deltaTime;

            //check if queen has starved
            if (hunger <= 0)
            {
                endState = new DyingState(sm, parent);
                Transition();
            }

            //check if queen is feed
            if (((QueenAnt)parent).feed >= 5)
            {
                Transition();
            }
        }
    }
}