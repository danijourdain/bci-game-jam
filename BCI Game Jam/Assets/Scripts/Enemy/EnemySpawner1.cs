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

    public int scale = 0;
    public void Start()
    {
        spawningEnabled = true;
        // enemies[4].enemyPrefab.GetComponent<enemy>().damage_amount = enemies[4].enemyPrefab.GetComponent<enemy>().basedmg;
        // enemies[3].enemyPrefab.GetComponent<enemy>().damage_amount = enemies[3].enemyPrefab.GetComponent<enemy>().basedmg;
        // enemies[2].enemyPrefab.GetComponent<enemy>().damage_amount = enemies[2].enemyPrefab.GetComponent<enemy>().basedmg;
        // enemies[4].enemyPrefab.GetComponent<enemy>().HP  =  enemies[4].enemyPrefab.GetComponent<enemy>().baseHP; 
        // enemies[3].enemyPrefab.GetComponent<enemy>().HP  =  enemies[3].enemyPrefab.GetComponent<enemy>().baseHP; 
        // enemies[2].enemyPrefab.GetComponent<enemy>().HP  =  enemies[2].enemyPrefab.GetComponent<enemy>().baseHP; 
        // // foreach (var enemyData in enemies)
        // {
        //     StartCoroutine(SpawnEnemyCoroutine(enemyData));
        // }
    }

    public void FixedUpdate()
    {
        coolDownTimer -= Time.deltaTime;
        
        if(spawningEnabled && coolDownTimer <= 0) {
            coolDownTimer = spawnCoolDown;
            foreach (var enemyData in enemies)
            {
                if(ShouldSpawn(enemyData.spawnChance, enemyData.enemyName))
                {
                    SpawnEnemy(enemyData);
                }   
            }
        } else if (coolDownTimer <= 0)
        {
            coolDownTimer = spawnCoolDown;
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

    private bool ShouldSpawn(float chance, string name)
    {
        float value = Random.value;
        return  value <= chance;
    }

    private void SpawnEnemy(EnemySpawnData data)
    {
       GameObject enemy = Instantiate(data.enemyPrefab, transform, false);
       enemy.GetComponent<enemy>().HP = data.enemyPrefab.GetComponent<enemy>().baseHP * 1.5f * scale;
       enemy.GetComponent<enemy>().damage_amount = data.enemyPrefab.GetComponent<enemy>().basedmg * 1.5f * scale;
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
