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
            Magic_bullets,
            sharp_bullets,
            sharp_bullets,
            rapid_fire,
            rapid_fire,
            vampiric_bullets,
            cooled_off,
            increased_magic,
            increased_magic,
            chunky,
            tanky,
            slippery,
            IncreasePlasmaBallLevel,
            IncreaseSawbladeLevel,
            BIG_MAN,
        };
    }


    public void sharp_bullets()
    {
        attack_script.damage += 5f; // increase damage by 1
    }

    public void Magic_bullets()
    {
        ability_script.magicDamage += 2f; // increase magic damage by 1
        attack_script.damage += 1f; // increase physical damage by 0.5
    }

    public void BIG_MAN(){
        health_script.Max_HP += 20; // increase max health by 20
        health_script.current_HP += 20; // heal for 20
        health_script.damage_reduction += 0.1f; // increase damage by 5
    }

    public void rapid_fire()
    {
        attack_script.shootInterval *= 0.9f; // reduce cooldown by 10%
    }

    public void vampiric_bullets()
    {
        Debug.LogWarning("NOT IMPLEMENTED");
        attack_script.lifeSteal += 0.1f; // increase life steal by 10%
    }

    private int cooldownLevel = 0;
    public void cooled_off()
    {
        ability_script.cooldownReduction = Mathf.Log(cooldownLevel + 1); // increase cooldown reduction by 10%
    }

    public void increased_magic()
    {
        ability_script.magicDamage += 2f; // increase magic damage by 1
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
    }

    public void IncreasePlasmaBallLevel()
    {
        ability_script.plasma_ball_level++;
    }

    public void GiveRandomPowerup()
    {
        int index = UnityEngine.Random.Range(0, powerups.Count);
        powerups[index].Invoke();
    }

    public void IncreaseLaserLevel()
    {
        ability_script.laser_beam_level++;
    }

    public void IncreaseElectricityChargeLevel()
    {
        ability_script.electricity_charge_level++;
    }

    public void IncreaseSlotMachineLevel()
    {
        ability_script.slot_machine_level++;
    }
}
