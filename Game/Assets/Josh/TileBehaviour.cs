using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private GridManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponentInParent<GridManager>();
        GetComponent<MeshRenderer>().enabled = false;
    }



    /// <summary>
    /// Highlight the tile by enabling its mesh renderer
    /// </summary>
    private void OnMouseEnter()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    /// <summary>
    /// Stop highlighting the tile
    /// </summary>
    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
