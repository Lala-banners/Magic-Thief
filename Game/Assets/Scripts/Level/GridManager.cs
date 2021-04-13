using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    #region Instance
    public static GridManager Instance = null;
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


    [SerializeField]
    private GameObject gridTile;
    private TileBehaviour[,] grid;

    //Algorithm parameters
    private List<TileBehaviour> openList;
    private List<TileBehaviour> closedList;
    //End of algorithm parameters

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
                grid[i, j] = Instantiate(gridTile, new Vector3(i * -tileSize + transform.position.x, 0, j * tileSize + transform.position.z), Quaternion.identity).GetComponent<TileBehaviour>();
                grid[i, j].SetCoords(i, j);
                grid[i, j].SetScale(new Vector3(tileSize, 0.1f, tileSize));
                grid[i, j].transform.parent = gameObject.transform;
                SetTileWalkable(grid[i, j]);
            }
        }
    }

    /// <summary>
    /// Changes whether the tile is active or not. A tile should be deactivated when a wall, closed door, or other unpathable items are on top of it
    /// </summary>
    /// <param name="tile">The tile being activated/deactivated</param>
    private void SetTileWalkable(TileBehaviour tile)
    {
        /*
        if(!Physics.Raycast(tile.transform.position, Vector3.up, 1.0f, LayerMask.GetMask("Room")))
        {
            tile.isWalkable = false;
            tile.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        */
    }

    /// <summary>
    /// Finds a path from one tile to another using a variation of the A* pathfinding algorithm
    /// </summary>
    /// <param name="start">Coordinates of the beginning of the path</param>
    /// <param name="destination">Coordinates of the end of the path</param>
    /// <returns>A list of tiles from start to end</returns>
    public List<TileBehaviour> FindPath(Vector2Int start, Vector2Int destination)
    {
        //This part is probably temporary/test stuff
        foreach(TileBehaviour tile in grid)
        {
            tile.Test(false);
        }
        //End of temp part

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

    /// <summary>
    /// Gets list of tiles surrounding another tile
    /// </summary>
    /// <param name="pathCoords">the coordinates of the original tile</param>
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

    /// <summary>
    /// Returns the tile path by working backwards
    /// </summary>
    /// <param name="endNode">The last node in the line</param>
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

    /// <summary>
    /// Returns the distance between to tiles
    /// </summary>
    private float CalculateDistance(TileBehaviour a, TileBehaviour b)
    {
        return Vector2Int.Distance(a.Coords, b.Coords);
    }

    /// <summary>
    /// Gets the tile with the lowest fCost from a list
    /// </summary>
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
}
