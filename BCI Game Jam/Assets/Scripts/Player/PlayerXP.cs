using System;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private FillBar xpBar;

    public static event Action OnLevelUp;

    private float currentXP = 0f;
    private readonly float xpToNextLevel = 100f;

    public void Start()
    {
        currentXP = 0;
        xpBar.SetFill(currentXP, xpToNextLevel);
    }

     void OnEnable()
    {
        EnemyEvents.OnEnemyKilled += GainXP;  // subscribe
    }

    void OnDisable()
    {
        EnemyEvents.OnEnemyKilled -= GainXP;  // always unsubscribe!
    }

    private void GainXP(float amount)
    {
        currentXP += amount;
        xpBar.SetFill(currentXP, xpToNextLevel);

        if (currentXP >= xpToNextLevel)
            LevelUp();
    }

    private void LevelUp()
    {
        currentXP -= xpToNextLevel;
        xpBar.SetFill(currentXP, xpToNextLevel);
        Debug.Log("LEVEL UP!");

        // trigger level up event
        OnLevelUp?.Invoke();
    }
}