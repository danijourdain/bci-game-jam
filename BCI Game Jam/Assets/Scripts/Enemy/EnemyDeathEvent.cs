using System;

public static class EnemyEvents
{
    public static event Action<float> OnEnemyKilled;

    public static void EnemyKilled(float xp)
    {
        OnEnemyKilled?.Invoke(xp);
    }
}