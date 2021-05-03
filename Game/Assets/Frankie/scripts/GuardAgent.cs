using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Agent
{
	public class GuardAgent : MonoBehaviour
	{
		public string ID;

		public bool DetectionTest = false;
		public bool ATTACKTEST = false;
		public Animator GuardAnimator;
		public bool Initialised = false;
		public State currentState;

		public float viewRadius;
		//public float viewRadiusMajor;
		[Range(0, 360)] public float ViewAngle;
		//public float ViewAngleMajor;
		public Color ViewConeColor;
		public MeshFilter viewMeshFilter;

		public Transform player;
		public LayerMask PlayerLayer;
		public LayerMask ObstacleMask;
		//public Transform ViewConeSource;

		public Mesh viewMesh;
		public int rayCount;
		public Vector3 DirFromAngle(float angle, bool angleIsGlobal)
		{
			if (!angleIsGlobal)
			{
				angle += transform.eulerAngles.y;
			}
			return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));

		}

		public Vector2Int AgentPos { get; set; }
		public List<TileBehaviour> path;
		public int maxMoveDistance;
		public Vector2Int playerCoords;
		public float moveSpeed;

		public bool oneTurnHasPassed = false;

		public IEnumerator agentMovement(GuardAgent agent)
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
					yield return null;
				}
				agent.transform.position = end;
			}
			agent.AgentPos = agent.path[agent.path.Count - 1].Coords;
		}
	}
}

