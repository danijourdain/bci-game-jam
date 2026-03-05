using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private BCIController bciController;
    [SerializeField] private EnemySpawner enemySpawner; 

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Init();
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
        
        // stop BCI stuff
        // bciController.StopFlashing();
    }

    public void StartGame()
    {
        // reset player health
        // reset abilities
        // restart spawning
        enemySpawner.Start();

        // remove UI stuff
        gameOverUI.SetActive(false);
    
        // start BCI stuff
        bciController.StartFlashing();
    }
}
