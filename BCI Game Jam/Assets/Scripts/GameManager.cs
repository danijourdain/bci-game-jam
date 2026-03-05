using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private BCIController bciController;
    [SerializeField] private EnemySpawner enemySpawner; 
    [SerializeField] private GameObject playerGO;
    private shoot_and_turn shooter;
    private health health;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Init();

            shooter = playerGO.GetComponent<shoot_and_turn>();
            health = playerGO.GetComponent<health>();
        }
    }

    void Update()
    {   
        //for debugging
        if(Keyboard.current.pKey.wasPressedThisFrame)
        {
            Instance.GameOver();
        }
    }

    private void Init()
    {
        Instance = this;
    }

    public void GameOver()
    {
        // enable game over UI
        gameOverUI.SetActive(true);
        
        // begin countdown
        gameOverUI.GetComponent<RestartCountdown>().BeginTimer();

        // disable enemies
        enemySpawner.Stop();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // disable shooting
        shooter.Stop();
        
        // stop BCI stuff
        // bciController.StopFlashing();
    }

    public void StartGame()
    {
        // reset player health
        health.Start();

        // reset abilities
        shooter.Start();

        // restart spawning
        enemySpawner.Start();

        // remove UI stuff
        gameOverUI.SetActive(false);
    
        // start BCI stuff
        bciController.StartFlashing();
    }
}
