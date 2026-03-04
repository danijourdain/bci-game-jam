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

    private bool ShouldDodge()
    {
        return Random.value <= dodge_chance;
    }

    public void TakeDamage(float damageAmount)
    {
        Debug.Log("PLAYER HIT");
        if(!ShouldDodge())
        {
            current_HP -= damageAmount * (1 - damage_reduction);
        }
        if(current_HP <= 0f)
        {
            Debug.Log("GAME OVER");
            current_HP = Max_HP;
        }
    }
}
