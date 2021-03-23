using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWPointClick : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Grid grid;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    // Update is called once per frame
    private void Update()
    {
        #region Grid Based Movement (atm only hover)
        Camera mainCamera = Camera.main;
        RaycastHit hit;
        //Debug.Log(1 << 6);

        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100000, 1 << 6))
        {
            transform.position = Vector3.MoveTowards(transform.position, hit.transform.position, moveSpeed * Time.deltaTime);
            //MoveOnGrid(hit.point);
        }
        #endregion
    }

    /// <summary>
    /// This function will force the player to the squares of the grid
    /// </summary>
    /// <param name="nearPoint">Nearest point on the grid</param>
    private void MoveOnGrid(Vector3 nearPoint)
    {
        //Vector3 finalPos = grid.GetNearestTiles(nearPoint);
        //transform.position = finalPos;
    }
    
}
