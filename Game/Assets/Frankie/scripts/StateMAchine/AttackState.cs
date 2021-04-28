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
        agent.ATTACKTEST = false;
        return pursueState;
        //return this;
    }
}
