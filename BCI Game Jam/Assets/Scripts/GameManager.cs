using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject levelUpUI;
    [SerializeField] private BCIController bciController;
    [SerializeField] private EnemySpawner enemySpawner; 
    [SerializeField] private GameObject playerGO;
    private shoot_and_turn shooter;
    private health health;

    public bool currentlyLevellingUp = false;

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

    void OnEnable()
    {
        PlayerXP.OnLevelUp += EnableLevelUpScreen;  // subscribe
    }

    void OnDisable()
    {
        PlayerXP.OnLevelUp -= EnableLevelUpScreen;  // always unsubscribe!
    }

    private void EnableLevelUpScreen()
    {
        Debug.Log("ENABLING SCREEN");
        currentlyLevellingUp = true;
        DisableGameplay();
        levelUpUI.SetActive(true);
        levelUpUI.GetComponent<LevelUpScreen>().DisplayPowerupsForSelect();
    }

    public void SelectPowerup(int index)
    {
        Debug.Log("SELECTED FROM GAME MANAGER " + index);
        levelUpUI.GetComponent<LevelUpScreen>().OnBCISelect(index);
        currentlyLevellingUp = false;
        ResumeGame();
    }

    void Update()
    {   
        //for debugging
        if(Keyboard.current.pKey.wasPressedThisFrame)
        {
            Instance.GameOver();
        }
        else if (Keyboard.current.aKey.wasPressedThisFrame && currentlyLevellingUp)
        {
            SelectPowerup(0);
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
        DisableGameplay();

        // stop BCI stuff
        bciController.StopFlashing();
    }

    private void DisableGameplay()
    {
        Debug.Log("DISABLING GAMEPLAY");
        // disable enemies
        enemySpawner.Stop();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // disable shooting
        shooter.Stop();
    }

    private void ResumeGame()
    {
        enemySpawner.Resume();
        shooter.Resume();
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
