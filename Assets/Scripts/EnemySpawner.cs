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

    IEnumerator Start()
    {
        game = FindObjectOfType<GameScript>();
        // Start the wave spawning coroutine.
        yield return StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        game.changeShips(1);
        var enemyInstance = Instantiate(enemyPrefab, transform.position,
                Quaternion.identity);
        
    }
}
