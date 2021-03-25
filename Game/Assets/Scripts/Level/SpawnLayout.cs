using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLayout : MonoBehaviour
{
    public static SpawnLayout Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    //Index 0 = type bottom/left
    //Index 1 = type left/right
    //Index 2 = type bottom/up
    //Index 3 = right/up
    public List<GameObject> room = new List<GameObject>(); //List of spawnable room layouts

    private void Start()
    {
        SpawnRandomRoom();
    }

    //Works! 
    //Going to put this script on the spawn points
    public void SpawnRandomRoom()
    {
        int random = Random.Range(1, room.Count); //Between room 1 and 2
        Instantiate(room[random], transform.position, Quaternion.identity);
    }
}
