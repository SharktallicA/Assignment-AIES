using Assets.Code;
using Assets.Code.FSM;
using UnityEngine;

/// <summary>
/// Entity subclass for Worker Ant agent
/// </summary>
class WorkerAnt : Entity
{
    /// <summary>
    /// Entity's DNA properties
    /// </summary>
    public DNA dna;

    /// <summary>
    /// Serves as a countdown until worker starves
    /// </summary>
    public float hunger = 100f;

    public override void derivedStart()
    {
        dna = new DNA();
        //Set initial state
        GetComponent<StateMachine>().ChangeState(new FindingFoodState(GetComponent<StateMachine>(), this));
    }

    public override void derivedUpdate()
    {
        //Rotate food (if present)
        if (transform.childCount > 1)
        {
            if (transform.GetChild(1).localRotation.eulerAngles != new Vector3(90, 0, 0))
                transform.GetChild(1).localRotation = Quaternion.Lerp(transform.GetChild(1).localRotation, Quaternion.Euler(new Vector3(90, 0, 0)), dna.speed * 3f * Time.deltaTime);
        }
    }

    /// <summary>
    /// FSM interruption clause to override what the Worker Ant does
    /// </summary>
    /// <param name="loc">Pheromone's origin position</param>
    public void GivePheromone(Vector3 loc)
    {
        GetComponent<StateMachine>().ChangeState(new AttractedState(GetComponent<StateMachine>(), this, GetComponent<StateMachine>().GetState(), loc));
    }
}