using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected GameManager system;

    public State(GameManager machine)
    {
        system = machine;
    }

    public virtual void OnEnter() { }
    public virtual void PlayerMove() { }
    public virtual void PlayerAction() { }
    public virtual void EnemyMove() { }
    public virtual void EnemyAction() { }
}
