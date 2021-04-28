using AI.Agent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/Idle")]

public class IdleState : State
{
    public DetectedState detectedState; 
    public override State RunCurrentState(GuardAgent agent, Animator animator)
    {
        Vector3 dirToPlayer = (agent.player.position - agent.transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(agent.transform.position, agent.player.position);
        agent.ViewConeColor = Color.yellow;
        //If the player is in the minor view cone
        if (Vector3.Angle(agent.transform.forward, dirToPlayer) < agent.ViewAngle / 2)
        {
            
            if (!Physics.Raycast(agent.transform.position, dirToPlayer, distanceToPlayer, agent.ObstacleMask) && distanceToPlayer < agent.viewRadius)
            {
                Debug.DrawLine(agent.transform.position, agent.player.position, Color.red);
                //there are no obstacles in the way of vision
                return detectedState;
            }
        }
        return this;
    }
}
