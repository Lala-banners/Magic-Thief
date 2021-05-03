using AI.Agent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/Attack")]

public class AttackState : State
{
    public DetectedState detectedState;
    public PursueState pursueState;
    public override State RunCurrentState(GuardAgent agent, Animator animator)
    {
        Debug.Log("I SPIT ON U AND UR FAMILY" + agent.ID);
        agent.path = GridManager.Instance.FindPath(agent.AgentPos, GameManager.Instance.Player.PlayerPosition);
        //if the agent is within the chase distance of player squares of the player
        if (agent.path.Count > 1)
        {
            return pursueState;
        }
        else
        {
            //return pursueState;
            return this;
        }
            
    }
}
