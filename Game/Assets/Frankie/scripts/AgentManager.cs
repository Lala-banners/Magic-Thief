using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Agent;

namespace AI.Herd
{
    public class AgentManager : MonoBehaviour
    {
        //BASE
        public GuardAgent AgentPrefab;
        List<GuardAgent> agents = new List<GuardAgent>();

        //SPAWNING
        [Header("Spawning")]
        public GameObject[] SpawnRooms;
        private Vector3 SpawnPos;
        [Range(1, 20)] public int GuardsPerRoom;
        [Range(10f, 100f)]public float spawnRadius = 50f;
        public float NegighborRadius = 1.0f;
        List<Vector3> agentPosList = new List<Vector3>();
        //DETECTION
        [Header("Detection")]
        [Range(10, 100)]public float DefaultViewRadius;
        [Range(0, 360)] public float DefaultViewAngle;

        public float meshResolution;
        public LayerMask PlayerLayer;
        public LayerMask ObstacleMask;
        public Transform player;

        public int raycountTotal;


        public int maxMoveDistance = 10;
        public float MoveSpeed = 5f;
        //this is a void so the rooms can load first and the guards can load after
        public void SpawnAgents()
        {
            
            //finds all the rooms tagged "Room"
            SpawnRooms = GameObject.FindGameObjectsWithTag("Room");
            //this loop runs for the max amount of guards in a room (1-5)
            for (int i = 0; i < GuardsPerRoom; i++)
            {
                int RoomIndex = 0;
                //this loop spawns 1 guard at every gameobject tagged as a room
                foreach (GameObject Spawn in SpawnRooms)
                {

                    //The spawn postion of each agent is radomised in a controlled radius, the y axis is locked at 0 so they dont float
                    SpawnPos = new Vector3(Random.insideUnitSphere.x * spawnRadius, 0f, Random.insideUnitSphere.z * 5) + Spawn.transform.position;
                    //spawns the agent at the postion just generated
                    GuardAgent newAgent = Instantiate(AgentPrefab, SpawnPos, Spawn.transform.rotation, transform);
                    RoomIndex++;
                    int Generation = i + 10;
                    newAgent.ID = Generation.ToString("X") + RoomIndex;
                    newAgent.name = "Guard " + newAgent.ID;
                    agentPosList.Add(newAgent.transform.position);
                    agents.Add(newAgent);
                    
                }
            }
        }
        public void Initialize()
        {
            foreach (GuardAgent agent in agents)
            {
                agent.ViewAngle = DefaultViewAngle;
                agent.viewRadius = DefaultViewRadius;
                agent.player = player;
                agent.PlayerLayer = PlayerLayer;
                agent.ObstacleMask = ObstacleMask;
                agent.viewMesh = new Mesh();
                agent.viewMesh.name = "View Mesh";
                agent.viewMeshFilter.mesh = agent.viewMesh;
                agent.moveSpeed = MoveSpeed;
                for (int i = 0; i < agentPosList.Count; i++)
                {
                    Vector3 NeighborCheck = (agentPosList[i] - agent.transform.position).normalized;
                    float distanceToNeighbor = Vector3.Distance(agent.transform.position, agentPosList[i]);
                    //if this doesnt work than change this to ! or add a layermask
                    if (!Physics.Raycast(agent.transform.position, NeighborCheck, distanceToNeighbor) && distanceToNeighbor < NegighborRadius)
                    {
                       
                        Debug.Log(agent.ID);
                    }
                }
                agent.AgentPos = new Vector2Int(0, 5);
                agent.maxMoveDistance = maxMoveDistance;
                agent.Initialised = true;
            }
        }
        private void Start()
        {
            SpawnAgents();
            Initialize();
        }
        private void Update()
        {
            foreach (GuardAgent agent in agents)
            {
                RunStateMachine(agent);

            }
            
        }
        private void LateUpdate()
        {
            foreach (GuardAgent agent in agents)
            {
                DrawFieldOfView(agent);
                raycountTotal = agent.rayCount * agents.Count;
            }
        }
        private void RunStateMachine(GuardAgent agent)
        {
            //the "nextState" is the current state's (but only if its not null) next state which is returned in the current states script 
            State nextState = agent.currentState?.RunCurrentState(agent, agent.GuardAnimator);
           
            // if (agent.currentState == nextState) return;
            
            //if the next state is not null
            if (nextState != null)
            {
                //switch to next state
                agent.currentState = nextState;
            }
        }
        public void DrawFieldOfView(GuardAgent agent)
        {
            int rayCount = Mathf.RoundToInt(agent.ViewAngle * meshResolution);
            float rayAngleSize = agent.ViewAngle / rayCount;
            List<Vector3> viewPoints = new List<Vector3>();
            for (int i = 0; i <= rayCount; i++)
            {
                float angle = agent.transform.eulerAngles.y - agent.ViewAngle / 2 + rayAngleSize * i;
                ViewCastInfo newViewCast = ViewCast(angle, agent);
                viewPoints.Add(newViewCast.point);
            }
            int vertexCount = viewPoints.Count + 1;
            Vector3[] verticies = new Vector3[vertexCount];
            int[] triangles = new int[(vertexCount - 2) * 3];

            verticies[0] = Vector3.zero;
            //verticies[0] = agent.transform.position;
            for (int i = 0; i < vertexCount - 1; i++)
            {
                verticies[i + 1] = agent.transform.InverseTransformPoint(viewPoints[i]);
                if (i < vertexCount - 2)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }
            agent.viewMesh.Clear();
            agent.viewMesh.vertices = verticies;
            agent.viewMesh.triangles = triangles;
            agent.viewMesh.RecalculateNormals(); 
            agent.rayCount = rayCount;
        }
        ViewCastInfo ViewCast(float globalAngle, GuardAgent agent)
        {
            Vector3 dir = agent.DirFromAngle(globalAngle, true);
            RaycastHit hit;
            if (Physics.Raycast(agent.transform.position, dir, out hit, agent.viewRadius, agent.ObstacleMask))
            {
                return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
            }
            else
            {
                return new ViewCastInfo(false, agent.transform.position + dir * agent.viewRadius, agent.viewRadius, globalAngle);
            }
        }
        public struct ViewCastInfo
        {
            public bool hit;
            public Vector3 point;
            public float dst;
            public float angle;

            public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
            {
                hit = _hit;
                point = _point;
                dst = _dst;
                angle = _angle;
            }
        }
    }

}
