using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PointClick : MonoBehaviour
{
    public LayerMask clickedOn; //to have a layer that differentiates on what can and cannot be clicked on
    private NavMeshAgent myAgent; //Reference to nav mesh agent component on fake player

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo, 100, clickedOn))
            {
                myAgent.SetDestination(hitInfo.point);
            }
        }
    }
}
