using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs a spawnear")]
    [SerializeField] private Enemy[] enemyPrefabs;        // arrastra Slime, Goblin…

    [Header("Puntos de spawn")]
    [SerializeField] private Transform[] spawnPoints;     // arrastra SpawnPoint_A/B/C

    [Header("Parámetros")]
    [SerializeField] private float firstDelay   = 2f;     // espera al empezar
    [SerializeField] private float spawnInterval = 5f;    // cada cuánto tiempo
    [SerializeField] private int   maxAlive     = 10;     // límite simultáneo

    private int aliveCount;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            if (aliveCount < maxAlive)
                SpawnOne();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnOne()
    {
        // 1. Elegir prefab y punto al azar
        Enemy prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 2. Instanciar
        Enemy e = Instantiate(prefab, point.position, Quaternion.identity);

        // 3. Contador
        aliveCount++;
        e.OnDeath += HandleEnemyDeath;     // nos suscribimos al evento de muerte
    }

    void HandleEnemyDeath()
    {
        aliveCount--;
    }
}
