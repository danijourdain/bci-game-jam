using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Information")]
    [SerializeField] private EnemySpawnData[] enemies;
    // [SerializeField] private int maxEnemiesAtOnce = 20;

    [Header("Path Information")]
    [SerializeField] private Transform[] pathStarts; 
    [SerializeField] private Transform pathEnd;
    public float spawnCoolDown;
    // private int currentEnemyCount = 0;
    private bool spawningEnabled = false;
    public float coolDownTimer;
    public void Start()
    {
        spawningEnabled = true;
        // foreach (var enemyData in enemies)
        // {
        //     StartCoroutine(SpawnEnemyCoroutine(enemyData));
        // }
    }

    public void Update()
    {
        foreach (var enemyData in enemies)
        {
            if(spawningEnabled)
            {
                if (coolDownTimer <= 0)
                {
                    coolDownTimer = spawnCoolDown;
                    if(ShouldSpawn(enemyData.spawnChance))
                    {
                        SpawnEnemy(enemyData);
                    }   
                }
                coolDownTimer -= Time.deltaTime;
            }
        }
    }
    // private IEnumerator SpawnEnemyCoroutine(EnemySpawnData data)
    // {
    //     while(spawningEnabled)
    //     {
    //         yield return new WaitForSeconds(data.spawnInterval);    // error here

    //         if(ShouldSpawn(data.spawnChance))
    //         {
    //             SpawnEnemy(data);
    //         }
    //     }
    // }

    private bool ShouldSpawn(float chance)
    {
        return Random.value <= chance;
    }

    private void SpawnEnemy(EnemySpawnData data)
    {
        Instantiate(data.enemyPrefab, transform, false);
    }

    public void Stop()
    {
        spawningEnabled = false;
    }

    public void Resume()
    {
        Start();
    }
}
