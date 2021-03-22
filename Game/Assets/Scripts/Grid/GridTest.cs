using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    public static GridTest Instance = null;
    #region Instance
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
    #endregion

    

    public void GetTiles(Transform destination, float max, float min = 0)
    {

    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

}
