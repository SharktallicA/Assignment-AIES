/*
    
    Feed Queen State

        Action:
            Instructs ant to return to the queen
        
        For:
            Worker Ant
        
        Starts from:
            Finding Food State
        
        Exits into:
            Feed Queen State (during feeding), Finding Food State (if forced to mate)

*/

using UnityEngine;

namespace Assets.Code.FSM
{
    /// <summary>
    /// Instructs ant to the queen
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
            Debug.Log(parent.transform.name + ": entered Find Queen State");
            base.Start();
            endState = new FeedQueenState(sm, parent);
            target = Object.FindObjectsOfType<QueenAnt>()[0];
        }

        /// <summary>
        /// Transit to queen ant
        /// </summary>
        public override void Update()
        {
            base.Update();

            if (parent.transform.childCount > 1)
            {
                //Rotate food if present
                if (parent.transform.GetChild(1).localRotation.eulerAngles != new Vector3(90, 0, 0))
                    parent.transform.GetChild(1).localRotation = Quaternion.Lerp(parent.transform.GetChild(1).localRotation, Quaternion.Euler(new Vector3(90, 0, 0)), ((WorkerAnt)parent).dna.speed * 2f * Time.deltaTime);
            }

            //Move towards queen
            if (parent.MoveTo(target.transform.position, ((WorkerAnt)parent).dna.sight, ((WorkerAnt)parent).dna.speed))
                Transition();
        }

        public override void Transition()
        {
            if (parent.transform.childCount == 1)
                endState = new FindingFoodState(sm, parent);
            base.Transition();
        }
    }
}