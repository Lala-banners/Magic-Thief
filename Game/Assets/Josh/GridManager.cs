using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gridTile;
    private TileBehaviour[,] grid;
    public Vector2Int PlayerPosition { get; private set; }

    //Algorithm parameters
    private List<TileBehaviour> openList;
    private List<TileBehaviour> closedList;
    //End of algorithm parameters

    [SerializeField]
    private List<Material> materials;

    [SerializeField]
    private int maxLength = 10;
    [SerializeField]
    private int maxHeight = 10;

    [SerializeField]
    private float tileSize = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        grid = new TileBehaviour[maxLength, maxHeight];
        for(int i = 0; i < maxLength; i++)
        {
            for(int j = 0; j < maxHeight; j++)
            {
                grid[i, j] = Instantiate(gridTile, new Vector3(i * tileSize, 0, j * tileSize), Quaternion.identity).GetComponent<TileBehaviour>();
                grid[i, j].SetCoords(i, j);
                grid[i, j].transform.parent = gameObject.transform;
                SetTileWalkable(grid[i, j]);
            }
        }
        PlayerPosition = new Vector2Int(0, 0);
    }

    /// <summary>
    /// Changes whether the tile is active or not. A tile should be deactivated when a wall, closed door, or other unpathable items are on top of it
    /// </summary>
    /// <param name="tile">The tile being activated/deactivated</param>
    private void SetTileWalkable(TileBehaviour tile)
    {
        if(!Physics.Raycast(tile.transform.position, Vector3.up, 1.0f, LayerMask.GetMask("Room")))
        {
            tile.isWalkable = false;
            tile.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public Material GetMaterial(int mat)
    {
        return materials[mat];
    }

    /// <summary>
    /// Finds a path from one tile to another using a variation of the A* pathfinding algorithm
    /// </summary>
    /// <param name="start">Coordinates of the beginning of the path</param>
    /// <param name="destination">Coordinates of the end of the path</param>
    /// <returns>A list of tiles from start to end</returns>
    public List<TileBehaviour> FindPath(Vector2Int start, Vector2Int destination)
    {
        TileBehaviour startNode = grid[start.x, start.y];
        TileBehaviour endNode = grid[destination.x, destination.y];

        openList = new List<TileBehaviour> { startNode };
        closedList = new List<TileBehaviour>();

        for(int i = 0; i < maxLength; i++)
        {
            for (int j = 0; j < maxHeight; j++)
            {
                TileBehaviour tile = grid[i, j];
                tile.gCost = int.MaxValue;
                tile.CalculateFCost();
                tile.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();

        while(openList.Count > 0)
        {
            TileBehaviour currentNode = GetLowestFCostNode(openList);
            if(currentNode.Coords == destination)
            {
                //Reached final node
                return CalculatePath(grid[destination.x, destination.y]);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(TileBehaviour neighbourNode in GetNeighbourList(currentNode.Coords))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                float tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neighbourNode);
                if(tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistance(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        //Out of nodes on the open list
        return null;
    }

    private List<TileBehaviour> GetNeighbourList(Vector2Int pathCoords)
    {
        List<TileBehaviour> neighbourList = new List<TileBehaviour>();

        //Left
        if(pathCoords.x > 0)
            neighbourList.Add(grid[pathCoords.x - 1, pathCoords.y]);
        //Right
        if(pathCoords.x < maxLength - 1)
            neighbourList.Add(grid[pathCoords.x + 1, pathCoords.y]);
        //Down
        if(pathCoords.y > 0)
        neighbourList.Add(grid[pathCoords.x, pathCoords.y - 1]);
        //Up
        if(pathCoords.y < maxHeight - 1)
            neighbourList.Add(grid[pathCoords.x, pathCoords.y + 1]);

        return neighbourList;
    }

    private List<TileBehaviour> CalculatePath(TileBehaviour endNode)
    {
        List<TileBehaviour> path = new List<TileBehaviour>
        {
            endNode
        };
        TileBehaviour currentNode = endNode;

        while(currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private float CalculateDistance(TileBehaviour a, TileBehaviour b)
    {
        return Vector2Int.Distance(a.Coords, b.Coords);
    }

    private TileBehaviour GetLowestFCostNode(List<TileBehaviour> pathNodeList)
    {
        TileBehaviour lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if(pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
