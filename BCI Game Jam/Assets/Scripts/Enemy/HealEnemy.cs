using UnityEngine;

public class HealEnemy : MonoBehaviour, IBaseEnemy
{
    public float damage_amount = 1.0f;
    public float HP = 1.0f;
    public float xpValue = 2.5f;

    public void TakeDamage(float damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0f)
        {
            Debug.Log("KILLED HEAL ENEMY");
            EnemyEvents.HealPlayer(1f); 
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