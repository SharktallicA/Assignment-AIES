using Assets.Code;
using Assets.Code.FSM;
using UnityEngine;

/// <summary>
/// 
/// </summary>
class QueenAnt : Entity
{
    /// <summary>
    /// 
    /// </summary>
    public DNA dna;

    /// <summary>
    /// Serves as a countdown until queen starves
    /// </summary>
    public float hunger = 100f;

    /// <summary>
    /// 
    /// </summary>
    public int feed = 1;

    /// <summary>
    /// 
    /// </summary>
    public int feedingThreshold = 5;

    public override void derivedStart()
    {
        dna = new DNA();
        GetComponent<StateMachine>().ChangeState(new FeedingState(GetComponent<StateMachine>(), this));
        transform.GetChild(0).GetComponent<Animator>().enabled = false;
        transform.GetChild(0).GetComponent<Animator>().speed = dna.speed / 2;
    }

    public override void derivedUpdate()
    {
        hunger -= (1f - dna.hunger) * Time.deltaTime;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="food"></param>
    public void GiveFood(Fungi food)
    {
        food.transform.SetParent(transform);
        food.GetComponent<StateMachine>().ChangeState(new DyingState(food.GetComponent<StateMachine>(), food));
        feed++;
        hunger = 100f;
    }
}