using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//Acordarse de esto
using UnityEngine.UI;

public class WaveUIController : MonoBehaviour
{
    [SerializeField] GameObject interwavePanel;
    [SerializeField] TextMeshProUGUI waveLabel;
    [SerializeField] Button continueBtn;
    [SerializeField] MiniShop shop;

    [SerializeField] GameObject finishPanel;      // Panel nuevo para el mensaje final
    [SerializeField] Button restartBtn;       // Botón reiniciar

    void Awake()
    {
        continueBtn.onClick.AddListener(() => WaveManager.Instance.StartNextWave());
        continueBtn.onClick.AddListener(() => shop.shopPanel.SetActive(false));
        restartBtn.onClick.AddListener(() => WaveManager.Instance.RestartGame());

        finishPanel.SetActive(false);  // Oculto el panel final al inicio
    }


    public void SetWaveText(int wave) => waveLabel.text = $"Wave {wave}";

    public void ShowInterwavePanel(int currentWave)
    {
        interwavePanel.SetActive(true);
        finishPanel.SetActive(false);
    }

    public void HideInterwavePanel()
    {
        interwavePanel.SetActive(false);
    }

    public void ShowFinishPanel()
    {
        interwavePanel.SetActive(false);
        finishPanel.SetActive(true);
    }
}

