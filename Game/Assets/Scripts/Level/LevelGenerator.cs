using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
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
        //To spawn level layout
        Instantiate(level, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
