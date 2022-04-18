using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to the EnemySpawner object.
/// It handles enemy wave-spawning logic.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private int enemiesToSpawn = 5;
    [SerializeField] private GameObject enemyPrefab = null;

    private GameScript game;

    private void Awake()
    {
        game = FindObjectOfType<GameScript>();
        game.changeShips(enemiesToSpawn);
    }

    IEnumerator Start()
    {
        // Start the wave spawning coroutine.
        yield return StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var enemyInstance = Instantiate(enemyPrefab, transform.position,
                Quaternion.identity);
    }
}
