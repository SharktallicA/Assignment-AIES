/*
    
    Dying State

        Action:
            Instructs worker ant to follow pheromone
        
        For:
            Worker Ant
        
        Starts from:
            Any state except Dying state
        
        Exits into:
            Last known state

*/

using UnityEngine;

namespace Assets.Code.FSM
{
    /// <summary>
    /// Instructs worker ant to follow pheromone
    /// </summary>
    public class AttractedState : State
    {
        /// <summary>
        /// 
        /// </summary>
        private Vector3 targetPos;

        public AttractedState(StateMachine nSM, Entity nParent, State previousStateInstance, Vector3 location) : base(nSM, nParent)
        {
            sm = nSM;
            parent = nParent;
            endState = previousStateInstance;
            targetPos = location;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Start()
        {
            if (sm.debug) Debug.Log(parent.transform.name + ": entered Attracted State");
            base.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Update()
        {
            base.Update();
            //Move towards pheromone source
            if (parent.MoveTo(targetPos, 0f, ((WorkerAnt)parent).dna.speed))
            {
                while (parent.transform.childCount > 1)
                    Object.FindObjectsOfType<QueenAnt>()[0].GetComponent<QueenAnt>().GiveFood(parent.transform.GetChild(1).GetComponent<Fungi>());
                Transition();
            }
        }
    }
}