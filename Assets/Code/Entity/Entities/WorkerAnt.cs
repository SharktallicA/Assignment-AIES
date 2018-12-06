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

    public override void derivedUpdate()
    {
        //Rotate food if present
        if (transform.childCount > 1)
        {
            if (transform.GetChild(1).localRotation.eulerAngles != new Vector3(90, 0, 0))
                transform.GetChild(1).localRotation = Quaternion.Lerp(transform.GetChild(1).localRotation, Quaternion.Euler(new Vector3(90, 0, 0)), dna.speed * 3f * Time.deltaTime);
        }
    }

    /// <summary>
    /// Interrupts what the ant is doing to make it make with the Queen
    /// </summary>
    public void GivePheromone(Vector3 loc)
    {
        GetComponent<StateMachine>().ChangeState(new AttractedState(GetComponent<StateMachine>(), this, GetComponent<StateMachine>().GetState(), loc));
    }
}