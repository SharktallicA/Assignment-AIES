using UnityEngine;

/// <summary>
/// 
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private Vector3 gridSize = new Vector3(20, 0, 20);

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private GameObject[] fungi;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private int fungiCount = 2;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private GameObject[] foliage;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private int foliageCount = 10000;

    private void Awake()
    {
        for (int i = 0; i < fungiCount; i++)
        {
            if (fungi.Length == 0) break;

            Vector3 pos = new Vector3(Random.Range(-gridSize.x / 2, gridSize.x / 2), 0, Random.Range(-gridSize.z / 2, gridSize.z / 2));
            Vector3 rot = new Vector3(0, Random.Range(0, 359), 0);

            GameObject obj = Instantiate(fungi[Random.Range(0, fungi.Length)], pos, Quaternion.Euler(rot)) as GameObject;
        }

        for (int i = 0; i < foliageCount; i++)
        {
            if (foliage.Length == 0) break;
            
            Vector3 pos = new Vector3(Random.Range(-gridSize.x, gridSize.x), 0, Random.Range(-gridSize.z, gridSize.z));
            
            GameObject obj = Instantiate(foliage[Random.Range(0, foliage.Length)], pos, Quaternion.Euler(new Vector3(0, 0, 0)), transform) as GameObject;
          
            obj.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(gridSize.x, 0.1f, gridSize.z));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Vector3 GetGridSize() { return gridSize; }
}