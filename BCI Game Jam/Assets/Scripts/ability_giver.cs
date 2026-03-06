using UnityEngine;

public class ability_giver : MonoBehaviour
{
    public health health_script;
    public ability ability_script;
    public shoot_and_turn attack_script;
    
    void health_example(int amount)
    {
        health_script.Max_HP += amount;
        health_script.current_HP += amount;
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

    void sharp_bullets()
    {
        ability_script.damage += 1f; // increase damage by 1
    }

    void rapid_fire()
    {
        ability_script.attackSpeed -= 0.1f; // reduce cooldown by 0.1 seconds
    }

    void vampiric_bullets()
    {
        ability_script.lifeSteal += 0.1f; // increase life steal by 10%
    }

    void cooled_off()
    {
        ability_script.cooldownReduction += 0.1f; // increase cooldown reduction by 10%
    }

    void increased_magic()
    {
        ability_script.magicDamage += 1f; // increase magic damage by 1
    }

    void chunky()
    {
        health_script.Max_HP += 10; // increase max health by 10
        health_script.current_HP += 10; // heal for 10
    }

    void tanky()
    {
        health_script.damage_reduction += 0.1f; // increase damage reduction by 10%
    }

    void slippery()
    {
        health_script.dodge_chance += 0.1f; // increase dodge chance by 10%
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
