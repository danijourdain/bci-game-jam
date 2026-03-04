using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Information")]
    [SerializeField] private EnemySpawnData[] enemies;
    [SerializeField] private int maxEnemiesAtOnce = 20;

    [Header("Path Information")]
    [SerializeField] private Transform[] pathStarts; 
    [SerializeField] private Transform pathEnd;

    private int currentEnemyCount = 0;

    void Start()
    {
        foreach (var enemyData in enemies)
        {
            Debug.Log("A");
            StartCoroutine(SpawnEnemyCoroutine(enemyData));
        }
    }

    private IEnumerator SpawnEnemyCoroutine(EnemySpawnData data)
    {
        while(true)
        {
            Debug.Log("B");
            yield return new WaitForSeconds(data.spawnInterval);
            Debug.Log("C");

            if(currentEnemyCount < maxEnemiesAtOnce && ShouldSpawn(data.spawnChance))
            {
                Debug.Log("D");
                SpawnEnemy(data);
            }
        }
    }

    private bool ShouldSpawn(float chance)
    {
        return Random.value <= chance;
    }

    // void Update()
    // {
    //     spawnTimer -= Time.deltaTime;
    //     if (spawnTimer <= 0)
    //     {

    //         spawnEnemies(spawnTimer, enemyTypes);
    //         spawnTimer = enemySpawnInterval;
    //     }
    // }

    private void SpawnEnemy(EnemySpawnData data)
    {
        Debug.Log("SPAWNING");
        GameObject newEnemy = Instantiate(data.enemyPrefab, transform, false);
        // GetPath path = newEnemy.GetComponent<GetPath>();
        // path.pathStarts = pathStarts;
        // path.pathEnd = pathEnd;
    }
}
