using Assets.Code.FSM;
using UnityEngine;

/// <summary>
/// 
/// </summary>
class Fungi : Entity
{
    /// <summary>
    /// 
    /// </summary>
    public bool taken = false;

    /// <summary>
    /// 
    /// </summary>
    private float test;

    /// <summary>
    /// 
    /// </summary>
    private Vector3 last;

    public override void derivedStart()
    {
        GetComponent<StateMachine>().ChangeState(new GrowingState(GetComponent<StateMachine>(), this));
    }

    public override void derivedUpdate()
    {
        test += 1f * Time.deltaTime;
        if (test >= 2f)
        {
            if (transform.position == last)
                taken = false;
            last = transform.position;
            test = 0;
        }
    }
}