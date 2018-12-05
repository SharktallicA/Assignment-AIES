/*
    
    Finding Food State

        Action:
            Instructs worker ant to find food for the queen
        
        For:
            Worker Ant
        
        Starts from:
            Entry point, Feed Queen state
        
        Exits into:
            Find Queen state (if food found), Dying state (if no food), Attracted state (if pheromone received)

*/

using UnityEngine;

namespace Assets.Code.FSM
{
    /// <summary>
    /// Instructs ant to find food for queen's consumption
    /// </summary>
    public class FindingFoodState : State
    {
        private void findFungi()
        {
            Fungi[] potentials = Object.FindObjectsOfType<Fungi>();

            //if no fungi is available, ant dies (RIP
            if (potentials.Length == 0)
            {
                endState = new DyingState(sm, parent);
                Transition();
                return;
            }

            //
            target = potentials[Random.Range(0, potentials.Length)];
        }

        /// <summary>
        /// 
        /// </summary>
        private void checkIfOneCloser()
        {
            Collider[] hitColliders = Physics.OverlapSphere(parent.transform.position, ((WorkerAnt)parent).dna.sight * 2);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].transform.parent.GetComponent<Fungi>())
                {
                    if (!hitColliders[i].transform.parent.GetComponent<Fungi>().taken)
                    {
                        target = hitColliders[i].transform.parent.GetComponent<Entity>();
                        return;
                    }
                }
            }
        }

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
            if (sm.debug) Debug.Log(parent.transform.name + ": entered Finding Food State");
            base.Start();
            endState = new FindQueenState(sm, parent);
            findFungi();
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
                findFungi();
                return;
            }

            //Ensure target is not taken
            if (((Fungi)target).taken)
            {
                findFungi();
                return;
            }

            //Check if any fungi are closer
            checkIfOneCloser();

            //Move towards fungi
            if (parent.MoveTo(target.transform.position, ((WorkerAnt)parent).dna.sight / 4, ((WorkerAnt)parent).dna.speed))
                Transition();
        }
       
        /// <summary>
        /// Collects fungi
        /// </summary>
        public override void Transition()
        {
            ((Fungi)target).taken = true;
            target.transform.SetParent(parent.transform);
            base.Transition();
        }
    }
}