using UnityEngine;

public class bullet: MonoBehaviour
{
    public float despawnTimer = 60.0f;
    public float damage_amount = 1.0f; // amount of damage this bullet does to the player
    public bool piercing = false; // whether the bullet can pierce through enemies or not

    public bool lifesteal = false; // whether the bullet can heal the player for a percentage of the damage dealt
    public float lifesteal_percentage = 0.1f; // the percentage of damage dealt that is converted to health for the player
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
                float heal_amount = damage_amount * lifesteal_percentage;
                health health = FindObjectOfType<health>();
                if (health != null)
                {
                    health.Heal(heal_amount);
                }
            }
            if (piercing == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
