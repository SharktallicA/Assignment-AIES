using UnityEngine;

namespace Assets.Code
{
    /// <summary>
    /// Representation of both Worker and Queen Ant's genetic data
    /// </summary>
    class DNA
    {
        /// <summary>
        /// Speed the ant can move at
        /// </summary>
        public float speed;

        /// <summary>
        /// Range the ant has see at
        /// </summary>
        public float sight;

        /// <summary>
        /// Rate in which the ant starves at
        /// </summary>
        public float hunger;

        public DNA()
        {
            //Randomly decide start values for DNA
            speed = Random.Range(2.95f, 3.05f);
            sight = Random.Range(2.95f, 3.05f);
            hunger = Random.Range(0.1f, 0.2f);
        }

        /// <summary>
        /// Returns an fitness based on averaged genetic data
        /// </summary>
        /// <returns></returns>
        public float GetFitness() { return ((speed + sight + hunger) / 3); }

        /// <summary>
        /// Adds variance to a random genetic variable
        /// </summary>
        public void Mutate()
        {
            float mutate = Random.Range(0.025f, 0.075f);

            switch (Random.Range(0, 2))
            {
                case 0:
                    speed *= Random.Range(1 - mutate, 1 + mutate);
                    break;
                case 1:
                    sight *= Random.Range(1 - mutate, 1 + mutate);
                    break;
                case 2:
                    hunger *= Random.Range(1 - mutate, 1 + mutate);
                    break;
            }
        }
    }
}