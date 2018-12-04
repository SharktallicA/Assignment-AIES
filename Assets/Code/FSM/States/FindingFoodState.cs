/*
    
    Finding Food State

        Action:
            Instructs ant to find food for queen's consumption
        
        For:
            Worker Ant
        
        Starts from:
            Entry, Feed Queen State
        
        Exits into:
            Feed Queen State

*/

using UnityEngine;

namespace Assets.Code.FSM
{
    /// <summary>
    /// Instructs ant to find food for queen's consumption
    /// </summary>
    public class FindingFoodState : State
    {
        public FindingFoodState(StateMachine nSM, Entity nParent) : base(nSM, nParent)
        {
            parent = nParent;
            sm = nSM;
        }

        /// <summary>
        /// Find fungi to eat as target
        /// </summary>
        public override void Start()
        {
            Debug.Log(parent.transform.name + ": entered Finding Food State");
            base.Start();
            endState = new FeedQueenState(sm, parent);

            Fungi[] potentials = Object.FindObjectsOfType<Fungi>();
            target = potentials[0];

            for (int i = 0; i < potentials.Length; i++)
            {
                if (Vector3.Distance(parent.transform.position, potentials[i].transform.position) < Vector3.Distance(parent.transform.position, target.transform.position))
                    target = potentials[i];
            }
        }

        /// <summary>
        /// Transit to fungi
        /// </summary>
        public override void Update()
        {
            base.Update();

            //Ensure target is present
            if (target == null)
            {
                Start();
                return;
            }

            //Ensure target is not taken
            if (((Fungi)target).taken)
            {
                Start();
                return;
            }

            if (parent.MoveTo(target.transform.position, ((WorkerAnt)parent).dna.sight, ((WorkerAnt)parent).dna.speed))
                Transition();
        }
       
        /// <summary>
        /// Collects fungi
        /// </summary>
        public override void Transition()
        {
            target.gameObject.transform.SetParent(parent.transform);
            ((Fungi)target).taken = true;
            base.Transition();
        }
    }
}