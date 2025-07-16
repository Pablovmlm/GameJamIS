using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject MMPanel;
    [SerializeField] GameObject TPanel;

    [Header("ButtonsMM")]
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button tutorialButton;

    [Header("Tutorial")]
    [SerializeField] Button backButton;

    // Start is called before the first frame update
    void Awake()
    {
        MMPanel.SetActive(true);
        TPanel.SetActive(false);
        startButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
        tutorialButton.onClick.AddListener(OpenTutorial);
        backButton.onClick.AddListener(BackToMenu);
    }

    void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void OpenTutorial()
    {
        TPanel.SetActive(true);
        MMPanel.SetActive(false);
        
    }

    void BackToMenu()
    {
        TPanel.SetActive(false);
        MMPanel.SetActive(true);
        
    }
}
