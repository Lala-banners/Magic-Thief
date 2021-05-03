using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Herd;
using AI.Agent;

public class EnemyTurn : GameState
{
    
    public EnemyTurn(GameManager machine) : base(machine)
    {
    }

    public override void OnEnter()
    {
        //Temporary code for testing
        system.TemporaryTesting();
    }

    public override void EnemyMove()
    {
        foreach (GuardAgent agent in AgentManager.Instance.agents)
        {
           AgentManager.Instance.RunStateMachine(agent);

        }

    }

    public override void EnemyAction()
    {
        base.EnemyAction();
    }

    public override void EndTurn()
    {
        system.enemyTurnsPassed++;
        system.SetState(new PlayerTurn(system));
    }
}
