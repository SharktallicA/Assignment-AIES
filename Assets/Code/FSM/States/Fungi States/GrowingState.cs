/*
    
    Growing State

        Action:
            Instructs fungi to grow to a certain size
        
        For:
            Fungi
        
        Starts from:
            Entry point, Sporing state
        
        Exits into:
            Sporing state (if predetermined size is reached), Dying state (if size is too big)

*/

using UnityEngine;

namespace Assets.Code.FSM
{
    /// <summary>
    /// Instructs fungi to grow to a certain size
    /// </summary>
    public class GrowingState : State
    {
        /// <summary>
        /// Scale target for the fungi to grow to during this state
        /// </summary>
        private float targetScale;

        public GrowingState(StateMachine nSM, Entity nParent) : base(nSM, nParent)
        {
            parent = nParent;
            sm = nSM;
        }

        /// <summary>
        /// Randomly specify target size
        /// </summary>
        public override void Start()
        {
            if (sm.debug) Debug.Log(parent.transform.name + ": entered Growing State");
            base.Start();
            endState = new SporingState(sm, parent);
            targetScale = parent.transform.localScale.x + Random.Range(0.1f, 0.25f);
        }

        /// <summary>
        /// Grows fungi over time
        /// </summary>
        public override void Update()
        {
            base.Update();

            //check if fungi is at critical size
            if (parent.transform.localScale.x > 1.5f)
            {
                endState = new DyingState(sm, parent);
                Transition();
            }

            //check if fungi needs to grow
            if (parent.transform.localScale.x < targetScale)
            {
                int rand = Random.Range(0, 75);
                if (rand == 50)
                    parent.transform.localScale += new Vector3(0.0125f, 0.0125f, 0.0125f);
            }
            else Transition();
        }
    }
}