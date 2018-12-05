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

        public DNA(float nSpeed = 2.5f, float nSight = 3f, float nHunger = 0.15f)
        {
            speed = nSpeed;
            sight = nSight;
            hunger = nHunger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public float GetFitness() { return ((speed + sight - hunger) / 3); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variance"></param>
        public void Mutate(float variance = 0.05f)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    speed *= Random.Range(1 - variance, 1 + variance);
                    break;
                case 1:
                    sight *= Random.Range(1 - variance, 1 + variance);
                    break;
                case 2:
                    hunger *= Random.Range(1 - variance, 1 + variance);
                    break;
            }
        }
    }
}