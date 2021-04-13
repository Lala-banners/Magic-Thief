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
    public virtual void PlayerMove(Vector2Int target) { }
    public virtual void PlayerAction() { }
    public virtual void EnemyMove() { }
    public virtual void EnemyAction() { }
    public virtual bool IsTurnOver() { return false; }
    public virtual void EndTurn() { }
}
