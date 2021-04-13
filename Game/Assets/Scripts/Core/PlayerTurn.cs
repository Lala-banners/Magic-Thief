using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : GameState
{
    private bool hasMoved = false;
    private bool hasActed = false;
    private bool turnPassed = false;

    public PlayerTurn(GameManager machine) : base(machine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void PlayerMove(Vector2Int target)
    {
        if (!hasMoved)
        {
            GameManager.Instance.Player.MoveToSpace(target);
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

    public override bool IsTurnOver()
    {
        return (hasActed && hasMoved) || turnPassed;
    }

    public override void EndTurn()
    {
        system.SetState(new EnemyTurn(system));
    }
}
