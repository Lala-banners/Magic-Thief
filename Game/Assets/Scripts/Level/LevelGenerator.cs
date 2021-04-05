using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //public List<GameObject> layout = new List<GameObject>(); //List of spawnable room layouts
    public GameObject level;

    private void Start()
    {
        GenerateMap();
    }

    //Testing!
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Transform rooms in transform)
                Destroy(rooms.gameObject);

            GenerateMap();
        }
    }*/

    public void GenerateMap()
    {
        //GameObject go = layout[Random.Range(0, layout.Count)];

        //To spawn level layout
        Instantiate(level, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
