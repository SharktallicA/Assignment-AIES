/*
    
    Mating State

        Action:
            Instructs queen ant to attract and mate with another ant
        
        For:
            Queen Ant
        
        Starts from:
            Feeding state
        
        Exits into:
            Feeding state

*/

using System.IO;
using UnityEngine;

namespace Assets.Code.FSM
{
    /// <summary>
    /// Instructs queen ant to attract and mate with another ant
    /// </summary>
    public class MatingState : State
    {
        /// <summary>
        /// 
        /// </summary>
        float mutationRate = 0.05f;

        /// <summary>
        /// 
        /// </summary>
        bool happened = false;

        public MatingState(StateMachine nSM, Entity nParent) : base(nSM, nParent)
        {
            parent = nParent;
            sm = nSM;
        }

        /// <summary>
        /// Perform roulette selection to acquire mate
        /// </summary>
        public override void Start()
        {
            if (sm.debug) Debug.Log(parent.transform.name + ": entered Mating State");
            base.Start();
            endState = new FeedingState(sm, parent);

            //Get all workers
            WorkerAnt[] potentials = Object.FindObjectsOfType<WorkerAnt>();

            //if there are more than one pontentials, perform roulette selection
            if (potentials.Length > 1)
            {
                //calculate initial fitness sum
                float fitnessSum = 0;
                for (int i = 0; i < potentials.Length; i++)
                    fitnessSum += potentials[i].dna.GetFitness();

                //Record fitness
                Debug.Log("Current population: " + fitnessSum);

                //generate number for roulette target
                float rouletteTarget = Random.Range(0, fitnessSum);
                float rouletteSum = 0;

                //work partial sum until selection
                for (int i = 0; i < potentials.Length; i++)
                {
                    rouletteSum += potentials[i].dna.GetFitness();
                    if (rouletteSum > rouletteTarget)
                        target = potentials[i];
                }
            }
            else target = potentials[0];

            ((WorkerAnt)target).GivePheromone(parent.transform.position);
        }

        /// <summary>
        /// Mate when worker is in range
        /// </summary>
        public override void Update()
        {
            if (Vector3.Distance(target.transform.position, parent.transform.position) < 0.25f && !happened)
            {
                Debug.Log("Making babies ;)");

                //Create new DNA strand based on a 50/50 splice of both parent's DNA
                DNA dna = new DNA();
                dna.speed = Random.Range(0f, 1f) < 0.5 ? ((QueenAnt)parent).dna.speed : ((WorkerAnt)target).dna.speed;
                dna.sight = Random.Range(0f, 1f) < 0.5 ? ((QueenAnt)parent).dna.sight : ((WorkerAnt)target).dna.sight;
                dna.hunger = Random.Range(0f, 1f) < 0.5 ? ((QueenAnt)parent).dna.hunger : ((WorkerAnt)target).dna.hunger;

                //Process any mutation
                if (Random.Range(0.000f, 1.000f) <= mutationRate) dna.Mutate();

                //Instantiate new offspring
                Vector3 newLocation = new Vector3(parent.transform.position.x + Random.Range(-5, 5), 0, parent.transform.position.z + Random.Range(-5, 5));
                Quaternion newRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                GameObject obj = Object.Instantiate(target.selfPrefab[0], newLocation, newRotation) as GameObject;
                obj.GetComponent<WorkerAnt>().dna = dna;

                Debug.Log("Speed: " + dna.speed + ", hunger: " + dna.hunger + ", sight: " + dna.sight);

                happened = true;

                Transition();
                return;
            }
        }
    }
}