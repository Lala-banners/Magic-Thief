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
        system.moveIndicator.interactable = true;
        system.actionIndicator.interactable = true;
        system.endTurnButton.interactable = true;
    }

    public override void PlayerMove(Vector2Int target)
    {
        if (!hasMoved)
        {
            hasMoved = system.Player.MoveToSpace(target);
            system.moveIndicator.interactable = !hasMoved;
            GridManager.Instance.HighlightPath(false, 0);
        }
        else
        {
            if (!hasActed)
            {
                hasActed = system.Player.MoveToSpace(target);
                system.actionIndicator.interactable = !hasActed;
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
            system.Player.ActionAnim(script);
            hasActed = true;
            system.actionIndicator.interactable = false;
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
        system.endTurnButton.interactable = false;
        system.actionIndicator.interactable = false;
        system.moveIndicator.interactable = false;
        system.playerTurnsPassed++;
        system.SetState(new EnemyTurn(system));
    }
}
