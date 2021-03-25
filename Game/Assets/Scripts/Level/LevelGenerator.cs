using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<Transform> startingPositions; //Where to spawn the layout
    private int direction;
    public float move;
    private float timeBetweenRoom;
    public float startTime;

    public float minX;
    public float maxX;
    public float minY;

    private void Start()
    {
        int randomStartPos = Random.Range(0, startingPositions.Count);
        transform.position = startingPositions[randomStartPos].position;
        //Spawn first room
        Instantiate(SpawnLayout.Instance.room[0], transform.position, Quaternion.identity);
    }

    public void MoveRoomLayout()
    {
        #region Move room right
        if (direction == 1 || direction == 2)
        {
            if(transform.position.x < maxX)
            {
                Vector3 newPosition = new Vector3(transform.position.x + move, transform.position.y);
                transform.position = newPosition;
            }
            else
            {
                direction = 5;
            }
        }
        #endregion

        #region Move room left

        #endregion

        #region Move room up

        #endregion
    }
}
