using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private GameObject pausePanel;
    private GameObject pauseButton;
    private GameObject resumeButton;
    public GameObject optionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        optionsPanel = GameObject.Find("OptionsPanel");
        pausePanel = GameObject.Find("PausePanel");
        pauseButton = GameObject.Find("PauseButton");

        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseResumeGame()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            pauseButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OptionsButton()
    {
        if (optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(false);
        }
        else
        {
            optionsPanel.SetActive(true);
        }
    }
}
