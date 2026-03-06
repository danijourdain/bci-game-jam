using System;
using Unity.VisualScripting;
using UnityEngine;

public class enemy : MonoBehaviour, IBaseEnemy 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float damage_amount = 1.0f; // amount of damage this enemy does to the player
    public float HP = 1.0f; // amount of health this enemy has

    public GameObject playerObject; // reference to the player GameObject
    private shoot_and_turn player; // reference to the player's shoot_and_turn script

    public void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<shoot_and_turn>();
    }
   
    public void TakeDamage(float damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0f)
        {
            player.XP += 10f; // award XP to player for defeating this enemy
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
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
