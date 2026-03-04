using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Spawner/Enemy Spawn Data")]
public class EnemySpawnData : ScriptableObject
{
    public GameObject enemyPrefab;
    public string enemyName;
    public float spawnInterval = 10f;
    public float spawnChance = 1f;
}