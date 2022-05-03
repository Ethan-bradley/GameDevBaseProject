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

    [SerializeField] private GameScript game;
    public GameObject target;

    private void Awake()
    {
        game = FindObjectOfType<GameScript>();
        game.changeShips(enemiesToSpawn);
    }

    IEnumerator Start()
    {
        if (target != null)
        {
            while (Vector3.Distance(this.transform.position, target.transform.position) > 2000)
            {
                yield return new WaitForSeconds(3.0f);
            }
        }
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
