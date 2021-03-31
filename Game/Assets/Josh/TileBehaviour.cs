using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private MeshRenderer mesh;
    private GridManager manager;
    private bool selected = false;
    private Vector2Int coords;

    //Parameters for the pathfinding algorithm
    public int gCost;
    public int hCost;
    public int fCost;

    public TileBehaviour cameFromNode;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        manager = GetComponentInParent<GridManager>();
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void SetCoords(int x, int y)
    {
        coords = new Vector2Int(x, y);
    }

    private void OnMouseDown()
    {
        selected = !selected;
        mesh.enabled = true;
        mesh.material = manager.GetMaterial(selected ? 1 : 0);
    }

    /// <summary>
    /// Highlight the tile by enabling its mesh renderer
    /// </summary>
    private void OnMouseEnter()
    {
        manager.FindPath(coords);
        mesh.enabled = true;
    }

    /// <summary>
    /// Stop highlighting the tile
    /// </summary>
    private void OnMouseExit()
    {
        mesh.enabled = selected;
    }
}
