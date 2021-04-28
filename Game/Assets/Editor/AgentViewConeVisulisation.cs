using AI.Agent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GuardAgent))]
public class AgentViewConeVisulisation : Editor
{
    private void OnSceneGUI()
    {
        GuardAgent agent = (GuardAgent)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(agent.transform.position, Vector3.up, Vector3.forward, 360, agent.viewRadius);
        Vector3 ViewAngleA = agent.DirFromAngle(-agent.ViewAngle / 2, false);
        Vector3 ViewAngleB = agent.DirFromAngle(agent.ViewAngle / 2, false);

        Handles.DrawLine(agent.transform.position, agent.transform.position + ViewAngleA * agent.viewRadius);
        Handles.DrawLine(agent.transform.position, agent.transform.position + ViewAngleB * agent.viewRadius);
    }
}
