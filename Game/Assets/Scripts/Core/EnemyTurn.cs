using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : GameState
{
    public EnemyTurn(GameManager machine) : base(machine)
    {
    }

    public override void OnEnter()
    {
        //Temporary code for testing
        system.currentState.EndTurn();
    }

    public override void EnemyMove()
    {
        base.EnemyMove();
    }

    public override void EnemyAction()
    {
        base.EnemyAction();
    }

    public override void EndTurn()
    {
        system.SetState(new PlayerTurn(system));
    }
}
