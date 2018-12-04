using Assets.Code;
using Assets.Code.FSM;

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
    public void GivePheromone()
    {
        GetComponent<StateMachine>().ChangeState(new FindQueenState(GetComponent<StateMachine>(), this));
    }
}