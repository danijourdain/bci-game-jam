using UnityEngine;

public class HealEnemy : MonoBehaviour, IBaseEnemy
{
    [SerializeField] private health playerHealth;

    public float damage_amount = 1.0f; 
    public float HP = 1.0f; 

    public void TakeDamage(float damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0f)
        {
            Destroy(gameObject);
            playerHealth.Heal(1f);
        }    
    }
}