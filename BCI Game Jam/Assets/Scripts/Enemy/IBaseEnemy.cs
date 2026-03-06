using UnityEngine;

public interface IBaseEnemy
{
    public void TakeDamage(float damageAmount);

    
    void OnTriggerEnter2D(Collider2D other);
}