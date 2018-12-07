using Assets.Code;
using Assets.Code.FSM;
using UnityEngine;

/// <summary>
/// Entity subclass for Queen Ant agent
/// </summary>
class QueenAnt : Entity
{
    /// <summary>
    /// Entity's DNA properties
    /// </summary>
    public DNA dna;

    /// <summary>
    /// Serves as a countdown until queen starves
    /// </summary>
    public float hunger = 100f;

    /// <summary>
    /// Count of food the queen has been given
    /// </summary>
    public int feed = 1;

    /// <summary>
    /// Threshold in which food is used for mating
    /// </summary>
    public int feedingThreshold = 5;

    public override void derivedStart()
    {
        dna = new DNA();
        //Set initial state
        GetComponent<StateMachine>().ChangeState(new FeedingState(GetComponent<StateMachine>(), this));
        //Disable start animation
        transform.GetChild(0).GetComponent<Animator>().enabled = false;
        transform.GetChild(0).GetComponent<Animator>().speed = dna.speed / 2;
    }

    public override void derivedUpdate()
    {
        hunger -= (1f - dna.hunger) * Time.deltaTime;
    }

    /// <summary>
    /// Allows Worker Ants to give food to the queen, resetting hunger and increases feed stat
    /// </summary>
    /// <param name="food">Reference to Fungi subclass as food</param>
    public void GiveFood(Fungi food)
    {
        food.transform.SetParent(transform);
        food.GetComponent<StateMachine>().ChangeState(new DyingState(food.GetComponent<StateMachine>(), food));
        feed++;
        hunger = 100f;
    }
}