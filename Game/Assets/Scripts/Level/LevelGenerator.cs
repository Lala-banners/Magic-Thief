using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> room = new List<GameObject>(); //List of spawnable room layouts
    public List<GameObject> hallways = new List<GameObject>(); //List of spawnable hallways

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
        GameObject go = room[Random.Range(0, room.Count)];
        Instantiate(go, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
