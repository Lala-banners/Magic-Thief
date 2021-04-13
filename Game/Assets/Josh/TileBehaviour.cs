using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private MeshRenderer mesh;
    private GridManager manager;
    public Vector2Int Coords { get; private set; }

    //Parameters for the pathfinding algorithm
    public float gCost;
    public float hCost;
    public float fCost;
    public bool isWalkable;
    public TileBehaviour cameFromNode;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        manager = GetComponentInParent<GridManager>();
        GetComponent<MeshRenderer>().enabled = false;
        isWalkable = true;
    }

    public void SetCoords(int x, int y)
    {
        Coords = new Vector2Int(x, y);
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    private void OnMouseDown()
    {
        //List<TileBehaviour> path = manager.FindPath(manager.PlayerPosition, Coords);
        //foreach(TileBehaviour tile in path)
        //{
        //  tile.Test(true);
        //}
        GameManager.Instance.PlayerMove(Coords);
    }

    /// <summary>
    /// Enables or disables mesh renderer.
    /// Testing code, mainly
    /// </summary>
    /// <param name="isMeshEnabled"></param>
    public void Test(bool isMeshEnabled)
    {
        mesh.enabled = isMeshEnabled;
    }
}
