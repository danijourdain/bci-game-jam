using System.Collections;
using UnityEngine;

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


    void Start()
    {
        StartCoroutine(spawnEnemies(spawnTimer, swarmPrefab));
    }

    private IEnumerator spawnEnemies(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        newEnemy.transform.parent = transform;
        newEnemy.transform.localScale = Vector3.one * 4f;
        StartCoroutine(spawnEnemies(enemySpawnInterval, enemy));
    }
}
