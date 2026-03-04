using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class FollowPath : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds5 = new(5);

    // walk speed
    [SerializeField] private float moveSpeed = 0.05f;

    // list of waypoints
    private Transform[] waypoints;

    // index of current waypoint
    private int nextWaypointIndex = 0;

    // reference to sprite renderer
    private SpriteRenderer spriteRenderer;

    private bool isMoving = false;

    public float laneOffset = 0.5f;
    private float veticalOffset = 0.65f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get waypoint from Path
        waypoints = GetPath.GetEnemyPath();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (waypoints.Length < 2)
        {
            throw new InvalidArraySizeException("Invalid length for waypoints. Needs to have at least 2 items");
        }


        GoToBeginning();
    }

    private void GoToBeginning()
    {
        waypoints = GetPath.GetEnemyPath();
        transform.position = waypoints[nextWaypointIndex].transform.position;
        Vector3 newPosition = transform.position;

        if (nextWaypointIndex == 0 || nextWaypointIndex == 3)
        {
            newPosition.x += Random.Range(-laneOffset, laneOffset);
        }
        else
        {
            newPosition.y += Random.Range(-veticalOffset, veticalOffset)*2;
        }
        
        transform.position = newPosition;
        nextWaypointIndex++;
        spriteRenderer.enabled = true;
        isMoving = true;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position-waypoints[nextWaypointIndex].transform.position);
        GetComponent<Rigidbody2D>().linearVelocity = -transform.up * moveSpeed;
    }

    // Update is called once per frame
    // void Update()
    // {
        
    //     // Move();
    // }

    // private void Move()
    // {
    //     if(!isMoving)
    //     {
    //         return;
    //     }

    //     // stop if reaching last waypoint
    //     if(nextWaypointIndex > waypoints.Length - 1)
    //     {
    //         FinishMove();
    //         return;
    //     }

    //     transform.position = Vector3.MoveTowards(transform.position, waypoints[nextWaypointIndex].transform.position, moveSpeed * Time.deltaTime);

    //     // if enemy reaches position of waypoint, increase index
    //     if(transform.position == waypoints[nextWaypointIndex].transform.position)
    //     {
    //         nextWaypointIndex++;
    //     }
    // }

    // private void FinishMove()
    // {
    //     isMoving = false;
    //     spriteRenderer.enabled = false;
    //     transform.position = new Vector3(0f, 0f, 0f);
    //         Debug.Log("Finished moving through waypoint");

    //     StartCoroutine(Restart());
    // }

    // private IEnumerator Restart()
    // {
    //     yield return _waitForSeconds5;
    //     nextWaypointIndex = 0;
    //     GoToBeginning();
    // }
}
