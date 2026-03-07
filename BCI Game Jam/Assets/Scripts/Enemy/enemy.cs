using UnityEngine;

public class enemy : MonoBehaviour, IBaseEnemy 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float damage_amount = 1.0f; // amount of damage this enemy does to the player
    public float HP = 1.0f; // amount of health this enemy has
    public float xpValue = 5f;
    public float baseHP = 1f;
    public float basedmg = 1f;
    public void Start()
    {
        damage_amount = basedmg;
    }
   
    public void TakeDamage(float damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0f)
        {
            EnemyEvents.EnemyKilled(xpValue);

            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<health>().TakeDamage(damage_amount);
        }
    }
}
