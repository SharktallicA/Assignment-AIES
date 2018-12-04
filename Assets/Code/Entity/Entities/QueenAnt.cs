using Assets.Code;
using Assets.Code.FSM;

/// <summary>
/// 
/// </summary>
class QueenAnt : Entity
{
    /// <summary>
    /// 
    /// </summary>
    public DNA dna;

    public override void derivedStart()
    {
        dna = new DNA();
        GetComponent<StateMachine>().ChangeState(new FeedingState(GetComponent<StateMachine>(), this));
    }

    /// <summary>
    /// 
    /// </summary>
    public int feed = 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="food"></param>
    public void GiveFood(Fungi food)
    {
        food.GetComponent<StateMachine>().ChangeState(new DyingState(food.GetComponent<StateMachine>(), food));
    }
}