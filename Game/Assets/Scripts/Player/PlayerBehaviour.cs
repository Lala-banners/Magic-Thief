using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Vector2Int PlayerPosition { get; private set; }
    /// <summary>
    /// This value is more for animation speed than anything
    /// </summary>
    [SerializeField] private float moveSpeed = 2.0f;

    private List<TileBehaviour> path;
    public bool IsMoving { get; private set; }

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        IsMoving = false;
        animator = GetComponent<Animator>();
        PlayerPosition = new Vector2Int(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToSpace(Vector2Int destination)
    {
        path = GridManager.Instance.FindPath(PlayerPosition, destination);
        if(path.Count > 0)
        {
            StartCoroutine(nameof(Movement));
        }
        else
        {
            //do something else
        }
    }

    private IEnumerator Movement()
    {
        IsMoving = true;
        animator.SetBool("Moving", IsMoving);
        for (int i = 1; i < path.Count; i++)
        {
            float increment = 0.0f;
            Vector3 start = transform.position;
            Vector3 end = path[i].transform.position;
            transform.rotation = Quaternion.LookRotation(end - start, Vector3.up);
            while (increment < 1.0f)
            {
                increment += Time.deltaTime * moveSpeed;
                transform.position = Vector3.Lerp(start, end, increment);
                yield return null;
            }
            transform.position = end;
        }
        PlayerPosition = path[path.Count - 1].Coords;
        IsMoving = false;
        animator.SetBool("Moving", IsMoving);
    }
}
