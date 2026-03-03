using UnityEngine;

public class ability_giver : MonoBehaviour
{
    public health health_script;
    public ability ability_script;
    public shoot_and_turn attack_script;
    
    void health_example(int amount)
    {
        health_script.max_health += amount;
        health_script.current_health += amount;
        // the health script has max_health, current_health, damage_reduction, and dodge_chance. 
    }

    void attack_example(float amount)
    {
        attack_script.damage += amount;
        // the attack script has damage, shoot_interval, shoot_delay, speed (bullet speed)
        // rotation_duration,  and range.
    }

    void ability_example()
    {
        // the ability script has damage, attackSpeed, accuracy and is_available
        ability_script.attackSpeed -= 1f; // reduce cooldown by 1 second
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
