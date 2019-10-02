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
        Debug.Log(Time.time);   
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

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

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
}
