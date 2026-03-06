using UnityEngine;


public class ability : MonoBehaviour
{
    [Header("Extra Damage")]
    public float lifeSteal = 0f; // percentage of damage dealt that is returned as health
    public float magicDamage = 0f; // additional damage that ignores armor
    public float cooldownReduction = 0f; // percentage reduction in cooldowns

    public bool is_available = true; // if the ability is unlocked or not
    public float speed = 10f;
    public int quadrant = 0; // 1 = top right, 2 = top left, 3 = bottom left, 4 = bottom right

    // abilites that fire unique magic projectiles and their toggles : 
    [Header("Sawblade")]
    public int sawblade_level = 0; // fires a sawblade that bounces off walls and hits multiple enemies
    public float sawblade_cooldown = 10f; // cooldown for the sawblade ability
    public float sawblade_timer = 0f; // timer for the sawblade ability
    public GameObject sawblade_projectile; // prefab for the sawblade projectile
    [Header("Ice Shard")]
    public int ice_shard_level = 0; // fires an ice shard that slows enemies
    public float ice_shard_cooldown = 5f; // cooldown for the ice shard ability
    public float ice_shard_timer = 0f; // timer for the ice shard ability
    [Header("Plasma Ball")]
    public int plasma_ball_level = 0; // fires a fireball that explodes on impact, dealing area damage
    public float plasma_ball_cooldown = 5f; // cooldown for the plasma ball ability
    public float plasma_ball_timer = 0f; // timer for the plasma ball ability
    public GameObject plasma_ball_projectile; // prefab for the plasma ball projectile
    [Header("Electricity Charge")]
    public int electricity_charge_level = 0; // fires a lightning bolt that chains between enemies
    public float electricity_charge_cooldown = 5f; // cooldown for the electricity charge ability
    public float electricity_charge_timer = 0f; // timer for the electricity charge ability
    public GameObject electricity_charge_projectile; // prefab for the electricity charge projectile
    [Header("Laser Beam")]
    public int laser_beam_level = 0; // fires a laser that pierces through enemies, hitting all in its path
    public float laser_beam_cooldown = 5f; // cooldown for the laser beam ability
    public float laser_beam_timer = 0f; // timer for the laser beam ability
    public GameObject laser_projectile; // prefab for the laser projectile
    [Header("Missile")]
    public int missile_level = 0; // fires a missile that homes in on the nearest enemy
    public float missile_cooldown = 5f; // cooldown for the missile ability
    public float missile_timer = 0f; // timer for the missile ability
    [Header("Grenade")]
    public int grenade_level = 0; // fires a grenade that explodes after a short
    public float grenade_cooldown = 5f; // cooldown for the grenade ability
    public float grenade_timer = 0f; // timer for the grenade ability
    [Header("Honored")]
    public int honored_level = 0; // fires a purple orb that does massive damage but has a long cooldown
    public float honored_cooldown = 10f; // cooldown for the honored ability
    public float honored_timer = 0f; // timer for the honored ability
    [Header("Gamma Knife")]
    public int gamma_knife_level = 0; // swings a gamma knife that ignores armor and has a chance to instantly kill enemies
    public float gamma_knife_cooldown = 5f; // cooldown for the gamma knife ability
    public float gamma_knife_timer = 0f; // timer for the gamma knife ability
    [Header("Jacob's Ladder")]
    public int jacobs_ladder_level = 0; // fires a lightning strike that hits all enemies pulsing arcs, dealing damage and stunning them
    public float jacobs_ladder_cooldown = 5f; // cooldown for the jacobs ladder ability
    public float jacobs_ladder_timer = 0f; // timer for the jacobs ladder ability

    void sawblade()
    {
        // code for firing a sawblade projectile that bounces off walls and hits multiple enemies
        if (sawblade_level > 0)
        {
            // instantiate sawblade projectile and set its properties based on sawblade_level
            sawblade_timer += Time.deltaTime;
            if (sawblade_timer >= sawblade_cooldown)
            {
                // fire sawblade projectile
                sawblade_timer = 0f;
                shoot_and_turn.Shoot(transform, quadrant, 1, magicDamage, sawblade_projectile);
            }

        }
    }
    void plasmaBall()
    {
        // code for firing a plasma ball projectile that explodes on impact, dealing area damage
        if (plasma_ball_level > 0)
        {
            // instantiate plasma ball projectile and set its properties based on plasma_ball_level
            plasma_ball_timer += Time.deltaTime;
            if (plasma_ball_timer >= plasma_ball_cooldown)
            {
                // fire plasma ball projectile
                plasma_ball_timer = 0f;
                plasma_ball_projectile.GetComponent<plasmaBall>().level = plasma_ball_level; // set the level of the plasma ball to determine its damage and size
                shoot_and_turn.Shoot(transform, quadrant, 1, magicDamage, plasma_ball_projectile);
            }

        }
    }
    void laser()
    {
        // code for firing a laser projectile that pierces through enemies, hitting all in its path
        if (laser_beam_level > 0)
        {
            // instantiate laser projectile and set its properties based on laser_beam_level
            laser_beam_timer += Time.deltaTime;
            if (laser_beam_timer >= laser_beam_cooldown)
            {
                // fire laser projectile
                laser_beam_timer = 0f;
                laser_projectile.GetComponent<laser>().level = laser_beam_level; // set the level of the laser to determine its damage and size
                shoot_and_turn.Shoot(transform, quadrant, 1, magicDamage, laser_projectile);
            }

        }
    }
    void electricityCharge()
    {
        // code for firing a lightning charge projectile that chains between enemies
        if (electricity_charge_level > 0)
        {
            // instantiate electricity charge projectile and set its properties based on electricity_charge_level
            electricity_charge_timer += Time.deltaTime;
            if (electricity_charge_timer >= electricity_charge_cooldown)
            {
                // fire electricity charge projectile
                electricity_charge_timer = 0f;
                electricity_charge_projectile.GetComponent<electricityCharge>().level = electricity_charge_level; // set the level of the electricity charge to determine its damage and size
                shoot_and_turn.Shoot(transform, quadrant, 1, magicDamage, electricity_charge_projectile);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        sawblade();
        plasmaBall();
        laser();
        electricityCharge();
    }
}

