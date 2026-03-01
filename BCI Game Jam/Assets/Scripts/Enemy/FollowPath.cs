using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FollowPath : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds5 = new(5);

    // path object
    [SerializeField] private GetPath pathContainer;

    // walk speed
    [SerializeField] private float moveSpeed = 0.05f;

    // list of waypoints
    private Transform[] waypoints;

    // index of current waypoint
    private int nextWaypointIndex = 0;

    // reference to sprite renderer
    private SpriteRenderer spriteRenderer;

    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get waypoint from Path
        waypoints = pathContainer.GetEnemyPath();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (waypoints.Length < 2)
        {
            throw new InvalidArraySizeException("Invalid length for waypoints. Needs to have at least 2 items");
        }

        GoToBeginning();
    }

    private void GoToBeginning()
    {
        waypoints = pathContainer.GetEnemyPath();
        transform.position = waypoints[nextWaypointIndex].transform.position;
        nextWaypointIndex++;
        spriteRenderer.enabled = true;
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(!isMoving)
        {
            return;
        }

        // stop if reaching last waypoint
        if(nextWaypointIndex > waypoints.Length - 1)
        {
            FinishMove();
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[nextWaypointIndex].transform.position, moveSpeed * Time.deltaTime);

        // if enemy reaches position of waypoint, increase index
        if(transform.position == waypoints[nextWaypointIndex].transform.position)
        {
            nextWaypointIndex++;
        }
    }

    private void FinishMove()
    {
        isMoving = false;
        spriteRenderer.enabled = false;
        transform.position = new Vector3(0f, 0f, 0f);
            Debug.Log("Finished moving through waypoint");

        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return _waitForSeconds5;
        nextWaypointIndex = 0;
        GoToBeginning();
    }
}
