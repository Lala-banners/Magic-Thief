using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float distanceFromPlayer = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Set the camera at the halfway point between the mouse and the player
        // This keeps the player's character on screen at all times, while letting
        //The player pan around the map
        gameObject.transform.position = Vector3.Lerp(player.position, MousePosition(), 0.5f);
    }

    /// <summary>
    /// Returns the world position of the mouse cursor, as far behind the camera as the camera is behind the player
    /// </summary>
    /// <returns></returns>
    private Vector3 MousePosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -distanceFromPlayer));
    }
}
