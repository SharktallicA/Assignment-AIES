/*
    
    Mating State

        Action:
            Instructs queen to summon a mate
        
        For:
            Queen Ant
        
        Starts from:
            Feeding State
        
        Exits into:
            Feeding State

*/

using System.IO;
using UnityEngine;

namespace Assets.Code.FSM
{
    /// <summary>
    /// Instructs queen to summon a mate
    /// </summary>
    public class MatingState : State
    {
        /// <summary>
        /// 
        /// </summary>
        private StreamWriter sW;

        /// <summary>
        /// 
        /// </summary>
        float mutationRate = 0.05f;

        public MatingState(StateMachine nSM, Entity nParent) : base(nSM, nParent)
        {
            parent = nParent;
            sm = nSM;
            sW = new StreamWriter("Assets/MatingLog.txt");
        }

        /// <summary>
        /// Perform roulette tournament selection to acquire mate
        /// </summary>
        public override void Start()
        {
            Debug.Log(parent.transform.name + ": entered Mating State");
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
                sW.WriteLine("Current population: " + fitnessSum);

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

            ((WorkerAnt)target).GivePheromone();
        }

        /// <summary>
        /// Mate when worker is in range
        /// </summary>
        public override void Update()
        {
            if (Vector3.Distance(target.transform.position, parent.transform.position) < 0.5f)
            {
                //Create new DNA strand based on a 50/50 splice of both parent's DNA
                DNA dna = new DNA();
                dna.speed = Random.Range(0f, 1f) < 0.5 ? ((QueenAnt)parent).dna.speed : ((WorkerAnt)target).dna.speed;
                dna.sight = Random.Range(0f, 1f) < 0.5 ? ((QueenAnt)parent).dna.sight : ((WorkerAnt)target).dna.sight;
                dna.hunger = Random.Range(0f, 1f) < 0.5 ? ((QueenAnt)parent).dna.hunger : ((WorkerAnt)target).dna.hunger;

                //Process any mutation
                if (Random.Range(0.000f, 1.000f) <= mutationRate) dna.Mutate();

                //Instantiate new offspring
                Vector3 newLocation = new Vector3(parent.transform.position.x + Random.Range(-5, 5), parent.transform.position.y, parent.transform.position.z + Random.Range(-5, 5));
                Quaternion newRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                GameObject obj = Object.Instantiate(target.selfPrefab[0], newLocation, newRotation) as GameObject;
                obj.GetComponent<WorkerAnt>().dna = dna;
                
                //Record new offspring
                sW.WriteLine("New offspring: " + dna.GetFitness());

                Transition();
            }
        }

        public override void Transition()
        {
            sW.Close();
            base.Transition();
        }
    }
}