using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    #region Rotations
    private Quaternion longHallwayRot = new Quaternion(0, 180, 0, 0); //to turn 180 degrees on y axis
    #endregion

    public List<GameObject> test = new List<GameObject>();
    public Transform spawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Change object being spawned rotation to the direction the doorway is facing
    /// </summary>
    public void TestSpawn()
    {
        GameObject testing = test[0];
        Instantiate(testing, spawn.position, longHallwayRot);
        Destroy(gameObject);
    }
}
