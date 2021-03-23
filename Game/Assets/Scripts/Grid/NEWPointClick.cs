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


        /*if (Input.GetMouseButtonDown(0))
        {*/
        isMoving = true;
        RaycastHit hit;
        Debug.Log(1 << 6);
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100000, 1 << 6))
        {
            transform.position = Vector3.MoveTowards(transform.position, hit.transform.position, moveSpeed * Time.deltaTime);
        }
        //}
    }
}
