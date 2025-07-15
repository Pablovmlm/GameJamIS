using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Añadir esto para poder reiniciar la escena
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [Header("Referencias")]
    [SerializeField] EnemySpawner spawner;
    [SerializeField] WaveUIController ui;

    [Header("Lista de Oleadas")]
    [SerializeField] WaveData[] waves;          // arrastra tus assets aquí
    [SerializeField] float timeBetweenWaves = 2f;

    int currentWave = 0;
    bool waveInProgress;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    void Start() => ShowWavePanel();            // Panel inicial “Oleada 1”

    void Update()
    {
        // Fin de oleada → todas las muertes las detecta EnemySpawner
    }

    /* ——— BOTÓN CONTINUAR ——— */
    public void StartNextWave()
    {
        currentWave++;

        waveInProgress = false;
        ui.HideInterwavePanel();
        ui.SetWaveText(currentWave);

        Invoke(nameof(BeginSpawning), timeBetweenWaves);
    }

    void BeginSpawning()
    {
        waveInProgress = true;
        spawner.StartSpawning(waves[currentWave - 1]);
    }

    /* ——— llamada desde EnemySpawner cuando no queda nadie vivo ——— */
    public void OnWaveEnemiesDefeated()
    {
        if (!waveInProgress) return;
        waveInProgress = false;
        if (currentWave >= waves.Length)
        {
            ui.ShowFinishPanel();        
        }
        else
        {
            ShowWavePanel();             // muestra “Oleada N+1”
        }
    }

    void ShowWavePanel()
    {
        ui.SetWaveText(currentWave + 1);
        ui.ShowInterwavePanel();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}



