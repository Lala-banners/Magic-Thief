using AI.Agent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/Pursue")]
public class PursueState : State
{
    public AttackState attackState;
    public IdleState IdleState;
    private List<TileBehaviour> path;
    public override State RunCurrentState(GuardAgent agent, Animator animator)
    {

        for (int i = 0; i < path.Count; i++)
        {
            float increment = 0.0f;
            Vector3 start = agent.transform.position;
            Vector3 end = path[i].transform.position;
            agent.transform.rotation = Quaternion.LookRotation(end - start, Vector3.up);
            while (increment < 1.0f)
            {
                increment += Time.deltaTime * agent.moveSpeed;
                agent.transform.position = Vector3.Lerp(start, end, increment);
                return null;
            }
            agent.transform.position = end;
        }

        return this;
  
    }
}
