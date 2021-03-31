using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gridTile;
    private TileBehaviour[,] grid;

    [SerializeField]
    private List<Material> materials;

    [SerializeField]
    private Vector2Int playerPos;
    [SerializeField]
    private List<Vector2Int> playerPositions;

    [SerializeField]
    private int maxLength = 10;
    [SerializeField]
    private int maxHeight = 10;

    [SerializeField]
    private float tileSize = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = new Vector2Int(0, 0);

        grid = new TileBehaviour[maxLength, maxHeight];
        for(int i = 0; i < maxLength; i++)
        {
            for(int j = 0; j < maxHeight; j++)
            {
                grid[i, j] = Instantiate(gridTile, new Vector3(i * tileSize, 0, j * tileSize), Quaternion.identity).GetComponent<TileBehaviour>();
                grid[i, j].SetCoords(i, j);
                grid[i, j].transform.parent = gameObject.transform;
                SetTileActive(grid[i, j].gameObject);
            }
        }
    }

    /// <summary>
    /// Changes whether the tile is active or not. A tile should be deactivated when a wall, closed door, or other unpathable items are on top of it
    /// </summary>
    /// <param name="tile">The tile being activated/deactivated</param>
    private void SetTileActive(GameObject tile)
    {
        //tile.SetActive(!Physics.Raycast(tile.transform.position, Vector3.up, 1.0f, LayerMask.GetMask("Room")));
    }

    public Material GetMaterial(int mat)
    {
        return materials[mat];
    }

    public void FindPath(Vector2Int destination)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
