using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
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
        manager = GetComponentInParent<GridManager>();
        isWalkable = true;
    }

    public void SetCoords(int x, int y)
    {
        Coords = new Vector2Int(x, y);
    }

    public void SetScale(Vector3 newScale)
    {
        transform.localScale = newScale;
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

    private void OnTriggerEnter(Collider other)
    {
        isWalkable = false;

    }

    private void OnTriggerExit(Collider other)
    {
        isWalkable = true;
    }
}
