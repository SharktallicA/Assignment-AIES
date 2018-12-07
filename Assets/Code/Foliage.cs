using UnityEngine;

/// <summary>
/// Used for generating scene foliage
/// </summary>
public class Foliage : MonoBehaviour
{
    /// <summary>
    /// Direction the plant faces
    /// </summary>
    private float direction;

    /// <summary>
    /// Speed in which the plant sways
    /// </summary>
    private float speed;

    /// <summary>
    /// Ayy
    /// </summary>
    private bool flip = true;

    private void Start()
    {
        direction = Random.Range(0, 359);
        speed = Random.Range(0.25f, 0.5f);
        transform.rotation = Quaternion.Euler(new Vector3(0, direction, 0));
    }

    private void Update()
    {
		if (flip)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(10, direction, 0)), speed * Time.deltaTime);
            if (transform.rotation == Quaternion.Euler(new Vector3(10, direction, 0)))
                flip = !flip;
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(-10, direction, 0)), speed * Time.deltaTime);
            if (transform.rotation == Quaternion.Euler(new Vector3(-10, direction, 0)))
                flip = !flip;
        }
	}
}
