using Assets.Code;
using Assets.Code.FSM;
using UnityEngine;

/// <summary>
/// 
/// </summary>
class WorkerAnt : Entity
{
    /// <summary>
    /// 
    /// </summary>
    public DNA dna;

    public override void derivedStart()
    {
       dna = new DNA();
       GetComponent<StateMachine>().ChangeState(new FindingFoodState(GetComponent<StateMachine>(), this));
    }

    /// <summary>
    /// Interrupts what the ant is doing to make it make with the Queen
    /// </summary>
    public void GivePheromone(Vector3 loc)
    {
        GetComponent<StateMachine>().ChangeState(new AttractedState(GetComponent<StateMachine>(), this, GetComponent<StateMachine>().GetState(), loc));
    }
}