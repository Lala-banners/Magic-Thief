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
        grid = new TileBehaviour[maxLength, maxHeight];
        for(int i = 0; i < maxLength; i++)
        {
            for(int j = 0; j < maxHeight; j++)
            {
                grid[i, j] = Instantiate(gridTile, new Vector3(i * -tileSize + transform.position.x, 0, j * tileSize + transform.position.z), Quaternion.identity).GetComponent<TileBehaviour>();
                grid[i, j].SetCoords(i, j);
                grid[i, j].SetScale(new Vector3(tileSize, 0.1f, tileSize));
                grid[i, j].transform.parent = gameObject.transform;
            }
        }
    }
    #endregion

    //Grid generation
    [SerializeField]
    private GameObject gridTile;
    [SerializeField]
    private int maxLength = 10;
    [SerializeField]
    private int maxHeight = 10;

    //Algorithm parameters
    private TileBehaviour[,] grid;
    private List<TileBehaviour> openList;
    private List<TileBehaviour> closedList;


    [SerializeField]
    private float tileSize = 1.0f;

    [SerializeField]
    private Sprite[] tileSprites;
    public List<TileBehaviour> highlightedPath;

    #region Pathfinding
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
            if(currentNode.coords == destination)
            {
                //Reached final node
                return CalculatePath(grid[destination.x, destination.y]);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(TileBehaviour neighbourNode in GetNeighbourList(currentNode.coords))
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
        return new List<TileBehaviour>();
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
        return Vector2Int.Distance(a.coords, b.coords);
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
    #endregion

    public void HighlightPath(bool isActive, int spriteIndex)
    {
        foreach (TileBehaviour tile in highlightedPath)
        {
            tile.SetSprite(isActive, tileSprites[spriteIndex]);
        }
    }
}
