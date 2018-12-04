using Assets.Code.FSM;

/// <summary>
/// 
/// </summary>
class Fungi : Entity
{
    /// <summary>
    /// 
    /// </summary>
    public bool taken = false;

    public override void derivedStart()
    {
        GetComponent<StateMachine>().ChangeState(new GrowingState(GetComponent<StateMachine>(), this));
    }
}