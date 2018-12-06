using UnityEngine;

namespace Assets.Code
{
    /// <summary>
    /// 
    /// </summary>
    class DNA
    {
        /// <summary>
        /// 
        /// </summary>
        public float speed;

        /// <summary>
        /// 
        /// </summary>
        public float sight;

        /// <summary>
        /// 
        /// </summary>
        public float hunger;

        public DNA()
        {
            speed = Random.Range(2.95f, 3.05f);
            sight = Random.Range(2.95f, 3.05f);
            hunger = Random.Range(0.1f, 0.2f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public float GetFitness() { return ((speed + sight + hunger) / 3); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variance"></param>
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