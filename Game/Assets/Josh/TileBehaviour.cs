using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private MeshRenderer mesh;
    private GridManager manager;
    private bool selected = false;
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
        selected = !selected;
        //mesh.enabled = true;
        mesh.material = manager.GetMaterial(selected ? 1 : 0);

        List<TileBehaviour> path = manager.FindPath(manager.PlayerPosition, Coords);
        foreach(TileBehaviour tile in path)
        {
            tile.Test();
        }
    }

    public void Test()
    {
        mesh.enabled = true;
    }

    /// <summary>
    /// Highlight the tile by enabling its mesh renderer
    /// </summary>
    private void OnMouseEnter()
    {
        //List<TileBehaviour> path = manager.FindPath(manager.PlayerPosition, Coords);
        //mesh.enabled = true;
    }

    /// <summary>
    /// Stop highlighting the tile
    /// </summary>
    private void OnMouseExit()
    {
        //mesh.enabled = selected;
    }
}
