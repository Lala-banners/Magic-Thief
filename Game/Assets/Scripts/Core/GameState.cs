using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected GameManager system;

    public GameState(GameManager machine)
    {
        system = machine;
    }

    public virtual void OnEnter() { }
    public virtual void PlayerMove() { }
    public virtual void PlayerAction() { }
    public virtual void EnemyMove() { }
    public virtual void EnemyAction() { }
    public virtual void EndTurn() { }
}
