using System.Collections;
using UnityEngine;

public class enemy : MonoBehaviour, IBaseEnemy 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float damage_amount = 1.0f; // amount of damage this enemy does to the player
    public float HP = 1.0f; // amount of health this enemy has
    public float xpValue = 5f;
    public float baseHP = 1f;
    public float basedmg = 1f;
    public SpriteRenderer sprite;
    Color starting;
    public void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        damage_amount = basedmg;
        starting = sprite.color;
    }
   
    // public void TakeDamage(float damage)
    // {
        
    // }
     public void TakeDamage(float damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0f)
        {
            EnemyEvents.EnemyKilled(xpValue);
            Destroy(gameObject);
        }
        StartCoroutine(changeColor());
        
    }

    public IEnumerator changeColor()
    {
        sprite.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(0.2f);
        sprite.color = starting;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<health>().TakeDamage(damage_amount);
        }
    }
}
