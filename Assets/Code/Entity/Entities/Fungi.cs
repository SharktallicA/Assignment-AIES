using Assets.Code.FSM;
using UnityEngine;

/// <summary>
/// Enity subclass for Fungi agent
/// </summary>
class Fungi : Entity
{
    /// <summary>
    /// Flags whether the entity is taken by another
    /// </summary>
    public bool taken = false;

    /// <summary>
    /// Internal countdown used for timed operations
    /// </summary>
    private float test;

    /// <summary>
    /// Last position tested
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
            //If fungi is found to not move but is flagged as taken, make sure this is corrected
            if (transform.position == last)
                taken = false;
            last = transform.position;
            test = 0;
        }
    }
}