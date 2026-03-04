using UnityEngine;

public class bullet: MonoBehaviour
{
    public float despawnTimer = 60.0f;
    public float damage_amount = 1.0f; // amount of damage this bullet does to the player
    void Update()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            Debug.Log("HIT AN ENEMY");
            other.gameObject.GetComponent<enemy>().TakeDamage(damage_amount);
            Destroy(gameObject);
        }
    }
}
