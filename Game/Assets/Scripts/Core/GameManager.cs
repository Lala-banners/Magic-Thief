using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : StateMachine
{
    #region Instance
    public static GameManager Instance = null;
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

    public PlayerBehaviour Player { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
        SetState(new PlayerTurn(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerMove(Vector2Int coords)
    {
        currentState.PlayerMove(coords);
    }
}
