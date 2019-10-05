using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private GameObject buttonPressSound;
    private GameObject pausePanel;
    private GameObject pauseButton;
    private GameObject resumeButton;
    public GameObject optionsPanel;
    public GameObject returnToMenuPanel;
    public GameObject controlsPanel;

    public float startTime;
    // Start is called before the first frame update
    void Start()
    {
        optionsPanel = GameObject.Find("OptionsPanel");
        pausePanel = GameObject.Find("PausePanel");
        pauseButton = GameObject.Find("PauseButton");

        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        buttonPressSound = GameObject.Find("ButtonPress");

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PauseResumeGame()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        if (Time.timeScale == 1f)
        {
            foreach (GameObject wasp in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                wasp.GetComponent<AudioSource>().Pause();
            }
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            pauseButton.SetActive(false);
        }
        else
        {
            foreach (GameObject wasp in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                wasp.GetComponent<AudioSource>().UnPause();
            }
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
        }
    }

    public void MainMenu()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        returnToMenuPanel.SetActive(true);

    }

    public void ReturnToMenuConfirm()
    {
        int totalTimePlayed = (int)(Time.time - startTime);
        PlayerPrefs.SetInt("timePlayed", PlayerPrefs.GetInt("timePlayed") + totalTimePlayed);

        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void ReturnToMenuDeny()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        returnToMenuPanel.SetActive(false);
    }

    public void ExitGame()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        int totalTimePlayed = (int)(Time.time - startTime);
        PlayerPrefs.SetInt("timePlayed", PlayerPrefs.GetInt("timePlayed") + totalTimePlayed);

        Application.Quit();
    }

    public void OptionsButton()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        if (optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(false);
        }
        else
        {
            optionsPanel.SetActive(true);
        }
    }

    public void DisplayControls()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        if (controlsPanel.activeSelf)
        {
            controlsPanel.SetActive(false);
        }
        else
        {
            controlsPanel.SetActive(true);
        }
    }
}
