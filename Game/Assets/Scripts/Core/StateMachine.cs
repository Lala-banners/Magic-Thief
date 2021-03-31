using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected GameState currentState;

    public void SetState(GameState state)
    {
        currentState = state;
        currentState.OnEnter();
    }
}
