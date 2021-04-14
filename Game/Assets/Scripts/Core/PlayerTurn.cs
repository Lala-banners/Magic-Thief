using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : GameState
{
    private bool hasMoved = false;
    private bool hasActed = false;
    private bool turnPassed = false;

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
            GameManager.Instance.Player.MoveToSpace(target);
            
            //uncomment this eventually //hasMoved = true;
        }
    }

    public override void PlayerAction(Interactable script)
    {
        if (!hasActed)
        {
            script.Interact();
            //uncomment this enevtually //hasActed = true;
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
