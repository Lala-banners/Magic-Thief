using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    #region Public
    public List<GameObject> room = new List<GameObject>(); //List of spawnable room layouts
    public int rows;
    public int columns;
    public int maxBT = 100; //bottom top room
    public int maxBR = 100; //bottom right room
    public int maxBL = 100; //bottom left room
    public int maxSH = 100; //Short hallway
    public int maxLH = 100; //Long hallway
    public int howManyStartRooms = 30;
    #endregion

    #region Private
    private int roomsToSpawn;
    private int startLayoutCount;
    private int bottomRightLayoutCount;
    private int bottomLeftLayoutCount;
    private int longHallwayCount;
    private int shortHallwayCount;
    private int resetRooms;
    #endregion

    private void Start()
    {
        resetRooms = howManyStartRooms;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Transform rooms in transform)
                rooms.gameObject.SetActive(false);
            
            GenerateMap();
        }
    }

    public void GenerateMap()
    {
        //Reset room layout types
        startLayoutCount = 0;
        bottomRightLayoutCount = 0;
        bottomLeftLayoutCount = 0;
        longHallwayCount = 0;
        shortHallwayCount = 0;

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                //Will generate rooms within Spawn Boundaries
                if (x >= 0 && x < 60 && y >= 0 && y < 60)
                {
                    roomsToSpawn = 1;
                    CheckLayout(roomsToSpawn);
                    GameObject go = Instantiate(room[roomsToSpawn], new Vector3(x, 0, y), Quaternion.identity);
                    go.transform.parent = transform;
                }
                else if (x == rows - 1 && y == columns - 1)
                {
                    roomsToSpawn = 0;
                    bottomRightLayoutCount++;
                    GameObject go = Instantiate(room[roomsToSpawn], new Vector3(x, 0, y), Quaternion.identity);
                    go.transform.parent = transform;
                }
                else
                {
                    Vector3 position = new Vector3(x, 0, y);
                    roomsToSpawn = Random.Range(0, 6);
                    CheckLayout(roomsToSpawn);
                    GameObject go = Instantiate(room[roomsToSpawn], new Vector3(x, 0, y), Quaternion.identity);
                    go.transform.parent = transform;
                }
            }
            Debug.Log("Bottom Top Rooms" + " " + startLayoutCount);
            Debug.Log("Bottom Left Rooms" + " " + bottomLeftLayoutCount);
            Debug.Log("Bottom Right Rooms" + " " + bottomRightLayoutCount);
            Debug.Log("Long Hallway" + " " + longHallwayCount);
            Debug.Log("Short Hallway" + " " + shortHallwayCount);
        }
    }

    public void CheckLayout(int random)
    {
        switch (random)
        {
            #region Bottom Right Layout Room
            case 0:
                //If bottom to right layout count is greater than the maximum
                if (bottomRightLayoutCount >= maxBR - 1)
                {
                    roomsToSpawn = 1; //Generate one room
                    startLayoutCount++; //Increase BR layout
                    howManyStartRooms = resetRooms;
                    break;
                }
                else if (howManyStartRooms <= 0) //before any more start rooms are spawned, have at least 1 bottom right rooms been spawned?
                {
                    bottomRightLayoutCount++;
                    howManyStartRooms = resetRooms;
                    break;
                }
                else
                {
                    roomsToSpawn = 1;
                    bottomRightLayoutCount++;
                    howManyStartRooms--;
                    break;
                }
            #endregion
            case 1:
                startLayoutCount++;
                howManyStartRooms--;
                break;
            #region Starting Room (Bottom top room layout)
            case 2:
                if (startLayoutCount >= maxBT)
                {
                    roomsToSpawn = 1;
                    startLayoutCount++;
                    howManyStartRooms--;
                    break;
                }
                else
                {
                    startLayoutCount++;
                    break;
                }
            #endregion
            #region Short Hallway
            case 3:
                if (shortHallwayCount >= maxSH)
                {
                    roomsToSpawn = 1;
                    startLayoutCount++;
                    howManyStartRooms--;
                    break;
                }
                else
                {
                    shortHallwayCount++;
                    break;
                }
            #endregion
            #region Long Hallway Layout
            case 4:
                if (longHallwayCount >= maxLH)
                {
                    roomsToSpawn = 1;
                    startLayoutCount++;
                    howManyStartRooms--;
                    break;
                }
                else
                {
                    longHallwayCount++;
                    break;
                }
            #endregion
            #region Bottom Left Room Layout
            case 5:
                if (bottomLeftLayoutCount > maxBL)
                {
                    roomsToSpawn = 1;
                    startLayoutCount++;
                    howManyStartRooms--;
                    break;
                }
                else
                {
                    bottomLeftLayoutCount++;
                    break;
                }
            #endregion
            default: //Default spawn room is start room (BT layout)
                startLayoutCount++;
                howManyStartRooms--;
                break;
        }
    }

    /*public void SpawnRoom()
    {
        int random = Random.Range(1, room.Count); //Excluding starting room

        int randomPos = Random.Range(0, spawnPoints.Count);
        transform.position = spawnPoints[randomPos].position;
        Instantiate(room[random], startingPosition.transform.position, transform.rotation);
    }*/

    /*public void SpawnHallway()
    {
        int r = Random.Range(0, hallway.Count); //Random hallway
        int position = Random.Range(0, spawnPoints.Count);
        transform.position = spawnPoints[position].position;
        Instantiate(hallway[r], startingPosition.transform.position, transform.rotation);
    }*/
}
