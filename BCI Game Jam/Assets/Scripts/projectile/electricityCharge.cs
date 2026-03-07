using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class electricityCharge: MonoBehaviour
{
    public float despawnTimer = 1f;
    public float damage_amount = 1.0f; // amount of damage this bullet does to the player
    public bool piercing = false; // whether the bullet can pierce through enemies or not
    public int level = 0; // level of the electricity charge, which determines its damage and size
    public int maxSegments = 5; // maximum number of segments that can be chained together
    public int timesHit = 0; // number of times the electricity charge has hit an enemy, used to determine how many segments to chain
    private bool hit = false; // whether the electricity charge has hit an enemy or not
    public bool isFirstSegment = true; // whether this is the first segment of the electricity charge or not, used to determine the initial direction of the charge

    void Start()
    {
        // set velocity to zero
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        if (isFirstSegment)
        {
            //play audio for the first segment of the electricity charge
            AudioManager.Instance.Play(AudioManager.SoundType.ElecticalCharge);
        }

        // //play audio for the electricity charge
        // AudioManager.Instance.Play(AudioManager.SoundType.ElecticalCharge);

        if (maxSegments > 0){
            float angle = Random.Range(-45f, 45f);
            Quaternion newDirection = Quaternion.Euler(0, 0, angle);
            newDirection = transform.rotation * newDirection;
            
            makeNewChargeSegment(transform.position, newDirection); // create the first segment of the electricity charge
        }
    }
    void Update()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    GameObject makeNewChargeSegment(Vector3 positionOfLastSegment, Quaternion directionOfLastSegment)
    {
        // make a new segment of the same size in front of the most recent one
        maxSegments -=1;
        GameObject newSegment = Instantiate(gameObject, transform.parent, false);
        newSegment.transform.position = positionOfLastSegment + transform.up/40; // set the position of the new segment to be in front of the last one
        newSegment.transform.rotation = directionOfLastSegment; // set the rotation of the new segment to be in the same direction as the last one
        // newSegment.GetComponent<electricityCharge>().maxSegments = maxSegments - 1;
        return newSegment;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<IBaseEnemy>().TakeDamage(damage_amount);
            hit = true;
            // make a new segment split into 2 is at random angles between -45 and 45 degrees from the direction of the last segment
                if (maxSegments > 0 && timesHit < level) // only chain if we haven't reached the maximum number of segments and the electricity charge has hit an enemy less times than its level
                {
                    timesHit++; // increase the timesHit counter to keep track of how many times the electricity charge has hit an enemy
                  
                    float angle = Random.Range(-90f, 90f);
                    Quaternion newDirection = Quaternion.Euler(0, 0, angle);
                    newDirection = transform.rotation * newDirection; // rotate the new direction by the rotation of the last segment
                    
                    GameObject branch = makeNewChargeSegment(transform.position, newDirection);
                    branch.GetComponent<electricityCharge>().timesHit = level;
                }
            if (piercing == false)
            {
                Destroy(gameObject);
            }
        }else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
