using System.Collections;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds5 = new(5);

    // array of waypoints to walk between
    [SerializeField] private Transform[] waypoints;

    // walk speed
    [SerializeField] private float moveSpeed = 2f;

    // index of current waypoint
    private int nextWaypointIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(waypoints.Length < 2)
        {
            throw new InvalidArraySizeException("Invalid length for waypoints. Needs to have at least 2 items");
        }

        transform.position = waypoints[nextWaypointIndex].transform.position;
        nextWaypointIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // stop if reaching last waypoint
        if(nextWaypointIndex > waypoints.Length - 1)
        {
            Debug.Log("Finished moving through waypoint");
            StartCoroutine(Restart());
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[nextWaypointIndex].transform.position, moveSpeed * Time.deltaTime);

        // if enemy reaches position of waypoint, increase index
        if(transform.position == waypoints[nextWaypointIndex].transform.position)
        {
            nextWaypointIndex++;
        }
    }

    private IEnumerator Restart()
    {
        yield return _waitForSeconds5;
        nextWaypointIndex = 0;
        Start();
    }
}
