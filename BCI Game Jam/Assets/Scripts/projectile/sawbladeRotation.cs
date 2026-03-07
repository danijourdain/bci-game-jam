using UnityEngine;

public class sawbladeRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float rotationSpeed = 360f; // degrees per second
    public GameObject Sawblade; // reference to the sawblade GameObject
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = rotationSpeed; // set the initial angular velocity of the sawblade
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
