using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    #region Instance
    public static Grid Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    [SerializeField] private float size = 1f;

    /// <summary>
    /// Getting the number of points on the grid.
    /// </summary>
    /// <param name="pos">Position of the grid points</param>
    /// <returns></returns>
    public Vector3 GetNearestTiles(Vector3 pos)
    {
        pos -= transform.position;

        int xCount = Mathf.RoundToInt(pos.x / size);
        int yCount = Mathf.RoundToInt(pos.y / size);
        int zCount = Mathf.RoundToInt(pos.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }


}
