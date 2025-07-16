using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    int aliveCount;
    public int AliveCount => aliveCount;

    bool isSpawning;
    public bool IsSpawning => isSpawning;

    Coroutine spawnRoutine;

    /* ——— llamado por WaveManager ——— */
    public void StartSpawning(WaveData wave)
    {
        if (spawnRoutine != null) StopCoroutine(spawnRoutine);
        spawnRoutine = StartCoroutine(SpawnWave(wave));
    }

    IEnumerator SpawnWave(WaveData wave)
    {
        isSpawning = true;

        foreach (var entry in wave.enemies)
        {
            for (int i = 0; i < entry.amount; i++)
            {
                Spawn(entry.prefab);
                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }

        isSpawning = false;
        CheckWaveEnd();
    }

    void Spawn(Enemy prefab)
    {
        Transform p = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Enemy e = Instantiate(prefab, p.position, Quaternion.identity);

        aliveCount++;
        e.OnDeath += () =>
        {
            aliveCount--;
            CheckWaveEnd();
        };
    }

    void CheckWaveEnd()
    {
        if (!isSpawning && aliveCount == 0)
            WaveManager.Instance.OnWaveEnemiesDefeated();
    }
}


