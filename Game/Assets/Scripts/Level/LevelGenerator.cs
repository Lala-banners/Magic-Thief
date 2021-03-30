using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> room = new List<GameObject>(); //List of spawnable room layouts
    public List<Transform> spawnPoints = new List<Transform>(); //List of spawn points
    public Transform startingPosition; //Where to start spawning the layout
    private int direction;
    public float move;
    private float timeBetweenRoom;
    public float startTime = 1f;

    public float minX; //Minimum X top border
    public float maxZ; //Maximum Z side border
    public float minY; //Minimum X bottom border

    private bool stopGeneration;

    private void Start()
    {
        int random = Random.Range(1, room.Count); //Excluding starting room

        int randomPos = Random.Range(0, spawnPoints.Count);
        transform.position = spawnPoints[randomPos].position;
        Instantiate(room[random], startingPosition.transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if(timeBetweenRoom <= 0 && stopGeneration == false)
        {

            timeBetweenRoom -= Time.deltaTime;
            
        }
        else
        {
            MoveRoomLayout();
            timeBetweenRoom = startTime;
        }
    }

    public void MoveRoomLayout()
    {
        #region MOVE ROOM RIGHT
        if (direction == 1 || direction == 2)
        {
            //If current z position is smaller than max Z (-65)
            if (transform.position.z < maxZ)
            {
                Vector3 newPosition = new Vector3(transform.position.x - move, transform.position.z);
                transform.position = newPosition;
            }
            else
            {
                direction = 5;
            }
        }
        #region Move room left
        else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > maxZ)
            {
                Vector3 newPosition = new Vector3(transform.position.x + move, transform.position.z);
                transform.position = newPosition;
            }
            else
            {
                direction = 5;
            }
        }
        else if(direction == 5)
        {
            if(transform.position.z < minY)
            {
                Vector3 newPosition = new Vector3(transform.position.x, transform.position.z + move);
                transform.position = newPosition;
            }
            else
            {
                //Stop level generation
                stopGeneration = true;
            }
        }
        #endregion
        #endregion
    }
}
