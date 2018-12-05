using System;
using Assets.Code.FSM;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Entity))]
public class StateMachine : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private State current;

    /// <summary>
    /// 
    /// </summary>
    public bool debug = false;

    private void Update()
    {
        if (current != null)
            current.Update();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nState"></param>
    public void ChangeState(State nState)
    {
        current = nState;
        current.Start();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public State GetState()
    {
        return current;
    }
}