using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public GameState currentState;

    public void SetState(GameState state)
    {
        currentState = state;
        currentState.OnEnter();
    }
}
