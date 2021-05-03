using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Button moveIndicator;
    public Button actionIndicator;
    public Button endTurnButton;

    public int playerTurnsPassed;
    public int enemyTurnsPassed;

    // Start is called before the first frame update
    void Start()
    {
        playerTurnsPassed = 0;
        enemyTurnsPassed = 0;
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
        SetState(new PlayerTurn(this));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.EnemyMove();
    }

    public void PlayerMove(Vector2Int coords)
    {
        currentState.PlayerMove(coords);
    }

    public void Activate(Interactable script)
    {
        currentState.PlayerAction(script);
    }

    public void EndTurn()
    {
        currentState.EndTurn();
    }

    public void TemporaryTesting()
    {
        StartCoroutine(nameof(TempCoroutine));
    }

    private IEnumerator TempCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        currentState.EndTurn();
    }
}
