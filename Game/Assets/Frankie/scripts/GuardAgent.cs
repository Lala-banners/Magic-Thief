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
	}
}

