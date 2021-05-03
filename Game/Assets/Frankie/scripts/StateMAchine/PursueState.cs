using AI.Agent;
using AI.Herd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/Pursue")]
public class PursueState : State
{
    public AttackState attackState;
    public IdleState IdleState;
    public DetectedState detectedState;
    
    
    public override State RunCurrentState(GuardAgent agent, Animator animator)
    {
        agent.path = GridManager.Instance.FindPath(agent.AgentPos, GameManager.Instance.Player.PlayerPosition);
        if (agent.maxMoveDistance >= agent.path.Count && agent.path.Count > 1)
        {
            agent.StartCoroutine(agent.agentMovement(agent));
            return this;
        }
        else if (agent.path.Count == 1)
        {
            return attackState;
        }
        else
        {
            return detectedState;
        }
           
  
    }
}
