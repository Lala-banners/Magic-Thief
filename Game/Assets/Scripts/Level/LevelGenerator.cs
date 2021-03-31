using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> room = new List<GameObject>(); //List of spawnable room layouts
    private Quaternion prefabRot = Quaternion.Euler(0, 90, 0); //Rotated 90 degrees on y axis
    private Connector connector;

    private void Start()
    {
        connector = GetComponent<Connector>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Transform rooms in transform)
                Destroy(rooms.gameObject);

            GenerateMap();
        }
    }

    public void GenerateMap()
    {
        if(connector.gameObject.CompareTag("Short"))
        {
            GameObject hallway = room[0];
            Instantiate(hallway, transform.position, prefabRot);
            Destroy(gameObject);
        }

        GameObject go = room[Random.Range(0, room.Count)];
        Instantiate(go, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
