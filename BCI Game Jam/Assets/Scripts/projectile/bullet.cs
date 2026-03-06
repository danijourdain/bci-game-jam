using UnityEngine;

public class bullet: MonoBehaviour
{
    public float despawnTimer = 60.0f;
    public float damage_amount = 1.0f; // amount of damage this bullet does to the player
    public bool piercing = false; // whether the bullet can pierce through enemies or not
    public bool lifesteal = false; // whether the bullet can steal life from enemies or not
    public float lifesteal_amount = 0.5f; // amount of health the player steals from enemies when hitting them with this bullet
    public health playerHealth; // reference to the player's health component
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
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<IBaseEnemy>().TakeDamage(damage_amount);
            if (lifesteal)
            {
                playerHealth.HandleHeal(lifesteal_amount);
            }
            if (piercing == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
