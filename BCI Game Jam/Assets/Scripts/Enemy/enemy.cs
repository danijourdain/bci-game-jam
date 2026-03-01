using UnityEngine;

public class enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float damage_amount = 1.0f; // amount of damage this enemy does to the player
    public float HP = 1.0f; // amount of health this enemy has
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        bullet damage_taken = other.gameObject.GetComponent<bullet>(); // destroy the bullet immediately
        HP -= damage_taken.damage_amount;
        Debug.Log("Current HP: " + HP);
        Destroy(other.gameObject);
        if (HP <= 0)
        {
           Destroy(gameObject);
        }
        
    }
}
