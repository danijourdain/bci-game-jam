using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[RequireComponent(typeof(health))]
[RequireComponent(typeof(ability))]
[RequireComponent(typeof(shoot_and_turn))]
public class ability_giver : MonoBehaviour
{
    private health health_script;
    private ability ability_script;
    private shoot_and_turn attack_script;

    private List<Action> powerups;
    
    void Start()
    {
        health_script = GetComponent<health>();
        ability_script = GetComponent<ability>();
        attack_script = GetComponent<shoot_and_turn>();

        // register all possible powerups
        powerups = new List<Action>
        {
            health_example,
            sharp_bullets,
            rapid_fire,
            vampiric_bullets,
            cooled_off,
            increased_magic,
            chunky,
            tanky,
            slippery,
            IncreasePlasmaBallLevel,
            IncreaseSawbladeLevel
        };
    }

    public void health_example()
    {
        float amount = 3f;
        health_script.Max_HP += amount;
        health_script.current_HP += amount;
        // the health script has max_health, current_health, damage_reduction, and dodge_chance. 
    }

    public void sharp_bullets()
    {
        attack_script.damage += 1f; // increase damage by 1
    }

    public void rapid_fire()
    {
        attack_script.shootInterval *= 0.9f; // reduce cooldown by 10%
    }

    public void vampiric_bullets()
    {
        Debug.LogWarning("NOT IMPLEMENTED");
        ability_script.lifeSteal += 0.1f; // increase life steal by 10%
    }

    public void cooled_off()
    {
        Debug.LogWarning("NOT IMPLEMENTED");
        ability_script.cooldownReduction += 0.1f; // increase cooldown reduction by 10%
    }

    public void increased_magic()
    {
        Debug.LogWarning("NOT IMPLEMENTED");
        ability_script.magicDamage += 1f; // increase magic damage by 1
    }

    public void chunky()
    {
        health_script.Max_HP += 10; // increase max health by 10
        health_script.current_HP += 10; // heal for 10
    }

    public void tanky()
    {
        if(health_script.damage_reduction < 1.0 ) { // increase damage reduction by 10% 
            health_script.damage_reduction += 0.1f;
        }
    }

    public void slippery()
    {
        if(health_script.dodge_chance < 1.0 ) { // increase dodge chance by 10% 
            health_script.dodge_chance += 0.1f;
        }
    }

    public void IncreaseSawbladeLevel()
    {
        ability_script.sawblade_level++;
        ability_script.sawblade_cooldown *= 0.9f;   // decrease cooldown by 10%
    }

    public void IncreasePlasmaBallLevel()
    {
        ability_script.plasma_ball_level++;
        ability_script.plasma_ball_cooldown *= 0.9f;  
    }
}
