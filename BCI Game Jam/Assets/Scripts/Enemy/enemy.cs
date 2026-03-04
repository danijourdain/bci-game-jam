using System;
using UnityEngine;

public class enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float damage_amount = 1.0f; // amount of damage this enemy does to the player
    public float HP = 1.0f; // amount of health this enemy has
   
    public void TakeDamage(float damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("ENEMY COLLISION WITH " + other.gameObject.name);
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
