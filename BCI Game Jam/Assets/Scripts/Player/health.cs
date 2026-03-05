using UnityEngine;

public class health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float Max_HP = 10f; 
    public float current_HP;
    public float damage_reduction = 0f; // percentage of damage reduced (0 = no reduction, 1 = immune)
    public float dodge_chance = 0f; // percentage chance to completely dodge an attack (0 = no dodge, 1 = always dodge)

    [SerializeField] private FillBar healthBarUI;
    
    public void Start()
    {
        current_HP = Max_HP;
        healthBarUI.SetFill(current_HP, Max_HP);
    }
    void OnEnable()
    {
        EnemyEvents.OnHealEnemyKilled += HandleHeal;
    }

    void OnDisable()
    {
        EnemyEvents.OnHealEnemyKilled -= HandleHeal;
    }

    private bool ShouldDodge()
    {
        return Random.value <= dodge_chance;
    }

    public void TakeDamage(float damageAmount)
    {
        if(!ShouldDodge())
        {
            current_HP -= damageAmount * (1 - damage_reduction);
            healthBarUI.SetFill(current_HP, Max_HP);
        }
        if(current_HP <= 0f)
        {
            GameManager.Instance.GameOver();
            current_HP = Max_HP;
        }
    }

    public void HandleHeal(float healAmount)
    {
        if(current_HP < Max_HP)
        {
            current_HP += healAmount;
            healthBarUI.SetFill(current_HP, Max_HP);
        }
    }
}
