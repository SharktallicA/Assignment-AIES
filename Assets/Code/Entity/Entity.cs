using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Entity : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    //private DNA dna;

    /// <summary>
    /// 
    /// </summary>
    public GameController controller;

    /// <summary>
    /// 
    /// </summary>
    public GameObject[] selfPrefab;

    /// <summary>
    /// Internal reference to Entity's Animator script
    /// </summary>
    protected Animator anim;

    /// <summary>
    /// 
    /// </summary>
    protected float tick = 0f;

    public virtual void derivedStart()
    {

    }

    public virtual void derivedUpdate()
    {

    }

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        derivedStart();
    }

    private void Update()
    {
        derivedUpdate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    public bool MoveTo(Vector3 targetPos, float targetRange = 0.1f, float speed = 1f, float modifier = 1f)
    {
        if (anim) anim.speed = speed;
        if (Vector3.Distance(targetPos, transform.position) > targetRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, (speed * modifier) * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetPos - transform.position, (speed * 2) * Time.deltaTime, 0.0f));
            if (anim) anim.enabled = true;
            return false;
        }
        else
        {
            if (anim) anim.enabled = false;
            return true;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void Tick(float nTick = 1)
    {
        tick += nTick;
        if (tick <= 0f) Object.Destroy(gameObject);
    }
}