using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject swarmPrefab;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnTimer = 3.5f;
    [SerializeField]
    private float enemySpawnInterval = 10f;
    [SerializeField]
    private bool horizontalSpawn = true;

    [SerializeField]
    public Transform[] pathStarts; 
    [SerializeField]
    public Transform pathEnd;
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnEnemies(spawnTimer, swarmPrefab);
            spawnTimer = enemySpawnInterval;
        }
    }

    private void spawnEnemies(float delay, GameObject enemy)
    {
        GameObject newEnemy = Instantiate(enemy, transform, false);
        GetPath path = newEnemy.GetComponent<GetPath>();
        path.pathStarts = pathStarts;
        path.pathEnd = pathEnd;
    }
}
