using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : GameState
{
    private bool hasMoved = false;
    private bool hasActed = false;

    //Add in something for the state the player is in - can only move when in move state, actions in action state etc.

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
            hasMoved = GameManager.Instance.Player.MoveToSpace(target);
            GridManager.Instance.HighlightPath(false, 0);
        }
        else
        {
            if (!hasActed)
            {
                hasActed = GameManager.Instance.Player.MoveToSpace(target);
                GridManager.Instance.HighlightPath(false, 0);
            }
        }
        if (IsTurnOver())
        {
            EndTurn();
        }
    }

    public override void PlayerAction(Interactable script)
    {
        if (!hasActed)
        {
            script.Interact();
            hasActed = true;
            if (IsTurnOver())
            {
                EndTurn();
            }
        }
    }

    public override bool IsTurnOver()
    {
        return hasActed && hasMoved;
    }

    public override void EndTurn()
    {
        Debug.Log("turn over");
        system.SetState(new EnemyTurn(system));
    }
}
