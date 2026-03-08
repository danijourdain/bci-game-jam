using System.Collections;
using UnityEngine;

public class HealEnemy : MonoBehaviour, IBaseEnemy
{
    public float damage_amount = 1.0f;
    public float HP = 1.0f;
    public float xpValue = 2.5f;
    Color basic;

    public SpriteRenderer sprite;
    public void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        basic = sprite.color;
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
        sprite.color = basic;
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