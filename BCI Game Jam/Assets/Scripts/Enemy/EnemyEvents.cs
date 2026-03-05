using System;
using UnityEngine;

public static class EnemyEvents
{
    public static event Action<float> OnEnemyKilled;
    public static event Action<float> OnHealEnemyKilled;

    public static void EnemyKilled(float xp)
    {
        OnEnemyKilled?.Invoke(xp);
    }

    public static void HealPlayer(float amount)
    {
        Debug.Log("INVOKING");
        OnHealEnemyKilled?.Invoke(amount);
    }
}