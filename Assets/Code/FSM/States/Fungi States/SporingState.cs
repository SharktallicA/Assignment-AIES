/*
    
    Sporing State

        Action:
            Instructs fungi to spawn another
        
        For:
            Fungi
        
        Starts from:
            Growing state
        
        Exits into:
            Growing state

*/

using UnityEngine;

namespace Assets.Code.FSM
{
    /// <summary>
    /// Instructs fungi to spore (reproduce)
    /// </summary>
    public class SporingState : State
    {
        public SporingState(StateMachine nSM, Entity nParent) : base(nSM, nParent)
        {
            parent = nParent;
            sm = nSM;
        }

        /// <summary>
        /// Randomly spawn a fungi
        /// </summary>
        public override void Start()
        {
            if (sm.debug) Debug.Log(parent.transform.name + ": entered Sporing State");
            base.Start();
            endState = new GrowingState(sm, parent);

            //Get and calculate the maximum distance between center origin and edge of the map
            int gridSizeX = (int)Mathf.Round(parent.controller.GetGridSize().x / 2);
            int gridSizeZ = (int)Mathf.Round(parent.controller.GetGridSize().z / 2);

            //Randomise new fungus' location and rotation

            float randX = 0;
            do
            {
                randX = parent.transform.position.x + Random.Range(-2, 2);
            } while (randX < -gridSizeX || randX > gridSizeX);

            float randZ = 0;
            do
            {
                randZ = parent.transform.position.z + Random.Range(-2, 2);
            } while (randZ < -gridSizeZ || randZ > gridSizeZ);


            Vector3 newLocation = new Vector3(randX, 0, randZ);
            Quaternion newRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 359), 0));

            //Instantiate new fungus
            Object.Instantiate(parent.selfPrefab[Random.Range(0, parent.selfPrefab.Length)], newLocation, newRotation);

            //Change state
            Transition();
        }
    }
}