using AI.Agent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/Start")]

public class StartState : State
{
    public IdleState IdleState;
    public override State RunCurrentState(GuardAgent agent, Animator animator)
    {
        //This will snap the guards to the grid, allign their rotation and other stuff

        if (agent.Initialised == true)
        {
            animator.Play("Guard Reboot");
            return IdleState;
        }
        else
        {
            return this;
        }
    }
}
