using UnityEngine;

public class health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float Max_HP = 10; 
    public float current_HP;
    public float damage_reduction = 0f; // percentage of damage reduced (0 = no reduction, 1 = immune)
    public float dodge_chance = 0f; // percentage chance to completely dodge an attack (0 = no dodge, 1 = always dodge)
    
    void Start()
    {
        current_HP = Max_HP;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        enemy damage_taken = other.gameObject.GetComponent<enemy>(); // destroy the bullet immediately
        current_HP -= damage_taken.damage_amount;
        Debug.Log("Current HP: " + current_HP);

        Destroy(other.gameObject);
        if (current_HP <= 0)
        {
            current_HP = Max_HP;
            
            // TODO: game over screen, restart level, etc.
        }
        
    }
}
