using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWPointClick : MonoBehaviour
{
    //Tile Holder for managing each tile
    [SerializeField] private Transform[] tiles;
    [SerializeField] private float moveSpeed = 5f;
    public bool isMoving = false;

    // Update is called once per frame
    void Update()
    {
        Camera mainCamera = Camera.main;
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            if (isMoving)
            {
                isMoving = true;
                Vector3.MoveTowards(transform.position, tiles[0].position, moveSpeed * Time.deltaTime);
            }
        }
    }
}
