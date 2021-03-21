using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    #region Grid Variables
    [SerializeField] private float size = 1f;
    #endregion

    public Vector3 NearestGridPoint(Vector3 pos)
    {
        pos -= transform.position;

        int countX = Mathf.RoundToInt(pos.x / size);
        int countY = Mathf.RoundToInt(pos.y / size);
        int countZ = Mathf.RoundToInt(pos.z / size);

        Vector3 result = new Vector3(
            countX * size,
            countY * size, 
            countZ * size);

        result += transform.position;
        return result;
    }

    // Implement this OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn
    private void OnDrawGizmos()
    {
        
    }


}
