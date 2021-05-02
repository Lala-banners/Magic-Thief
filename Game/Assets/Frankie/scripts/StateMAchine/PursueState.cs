using AI.Agent;
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
        //if the agent is within 10 squares of the player
        if (agent.maxMoveDistance >= agent.path.Count && agent.path.Count > 1)
        {
            for (int i = 1; i < agent.path.Count; i++)
            {
                float increment = 0.0f;
                Vector3 start = agent.transform.position;
                Vector3 end = agent.path[i].transform.position;
                agent.transform.rotation = Quaternion.LookRotation(end - start, Vector3.up);
                while (increment < 1.0f)
                {
                    increment += Time.deltaTime * agent.moveSpeed;
                    agent.transform.position = Vector3.Lerp(start, end, increment);
                }
                agent.transform.position = end;
            }
            agent.AgentPos = agent.path[agent.path.Count - 1].Coords;
            return this;
        }
        else if (agent.path.Count < 2)
        {
            return attackState;
        }
        else
        {
            return detectedState;
        }
           
  
    }
}
