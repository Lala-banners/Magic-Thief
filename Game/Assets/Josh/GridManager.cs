using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gridTile;
    private GameObject[,] grid;

    [SerializeField]
    private int playerPositionx;
    [SerializeField]
    private int playerPositionY;

    [SerializeField]
    private int maxLength = 10;
    [SerializeField]
    private int maxHeight = 5;

    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[maxLength, maxHeight];
        for(int i = 0; i < maxLength; i++)
        {
            for(int j = 0; j < maxHeight; j++)
            {
                grid[i, j] = Instantiate(gridTile, new Vector3(i, 0, j), Quaternion.identity);
                grid[i, j].transform.parent = gameObject.transform;
                SetTileActive(grid[i, j]);
            }
        }
    }

    private void SetTileActive(GameObject tile)
    {
        tile.SetActive(!Physics.Raycast(tile.transform.position, Vector3.up, 1.0f, LayerMask.GetMask("Room")));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
