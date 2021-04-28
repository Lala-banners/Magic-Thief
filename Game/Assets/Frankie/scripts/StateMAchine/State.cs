using AI.Agent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public abstract State RunCurrentState(GuardAgent agent, Animator animator);
}
