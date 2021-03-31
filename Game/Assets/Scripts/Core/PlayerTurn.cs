using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : GameState
{
    private bool hasMoved = false;
    private bool hasActed = false;

    public PlayerTurn(GameManager machine) : base(machine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void PlayerMove()
    {
        if (!hasMoved)
        {
            //Insert movement here
            hasMoved = true;
        }
    }

    public override void PlayerAction()
    {
        if (!hasActed)
        {
            //Insert action here
            hasActed = true;
        }
    }

    public override void EndTurn()
    {
        system.SetState(new EnemyTurn(system));
    }
}
