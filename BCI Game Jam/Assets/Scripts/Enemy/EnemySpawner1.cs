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

    // private int currentEnemyCount = 0;
    private bool spawningEnabled = false;

    public void Start()
    {
        spawningEnabled = true;
        foreach (var enemyData in enemies)
        {
            StartCoroutine(SpawnEnemyCoroutine(enemyData));
        }
    }

    private IEnumerator SpawnEnemyCoroutine(EnemySpawnData data)
    {
        while(spawningEnabled)
        {
            yield return new WaitForSeconds(data.spawnInterval);    // error here

            if(ShouldSpawn(data.spawnChance))
            {
                SpawnEnemy(data);
            }
        }
    }

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
}
