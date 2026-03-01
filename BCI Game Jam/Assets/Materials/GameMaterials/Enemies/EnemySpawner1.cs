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


    void Start()
    {
        StartCoroutine(spawnEnemies(spawnTimer, swarmPrefab));
    }

    private IEnumerator spawnEnemies(float interval, GameObject enemy)
    {   

        float randInterval = Random.Range(0.5f, 1.5f) * interval;
        yield return new WaitForSeconds(randInterval);
        if (horizontalSpawn)
        {
            GameObject newEnemy = Instantiate(enemy, (transform.position), Quaternion.identity);
            newEnemy.transform.position = new Vector3(newEnemy.transform.position.x + Random.Range(-20f, 20f), transform.position.y, 0f);
            newEnemy.transform.parent = transform;
            newEnemy.transform.localScale = Vector3.one * 4f;
        }
        else
        {
            GameObject newEnemy = Instantiate(enemy, (transform.position), Quaternion.identity);
            newEnemy.transform.position = new Vector3(transform.position.x, newEnemy.transform.position.y + Random.Range(-20f, 20f), 0f);
            newEnemy.transform.parent = transform;
            newEnemy.transform.localScale = Vector3.one * 4f;
        }
        StartCoroutine(spawnEnemies(enemySpawnInterval, enemy));
    }
}
