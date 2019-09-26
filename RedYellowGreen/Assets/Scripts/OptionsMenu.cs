using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private GameObject musicController;
    public GameObject soundButton, musicButton;
    public GameObject optionsPanel;
    private GameObject buttonPressSound;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            musicController = GameObject.FindWithTag("MusicController");
        }
        catch (System.Exception e)
        {
            Debug.Log("Can't assign music controller to object");
        }

        buttonPressSound = GameObject.Find("ButtonPress");
        InitialOptionsMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitialOptionsMenu()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            soundButton.GetComponent<Text>().text = "ON";
        }
        else if (PlayerPrefs.GetInt("isSoundEnabled") == 0)
        {
            soundButton.GetComponent<Text>().text = "OFF";
        }

        if (PlayerPrefs.GetInt("isMusicEnabled") == 1)
        {
            musicButton.GetComponent<Text>().text = "ON";
        }
        else if (PlayerPrefs.GetInt("isMusicEnabled") == 0)
        {
            musicButton.GetComponent<Text>().text = "OFF";
        }
    }

    public void SoundUpdate()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        if (soundButton.GetComponent<Text>().text == "ON")
        {
            soundButton.GetComponent<Text>().text = "OFF";
            PlayerPrefs.SetInt("isSoundEnabled", 0);
        }
        else if (soundButton.GetComponent<Text>().text == "OFF")
        {
            soundButton.GetComponent<Text>().text = "ON";
            PlayerPrefs.SetInt("isSoundEnabled", 1);
        }
    }

    public void MusicUpdate()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        if (musicButton.GetComponent<Text>().text == "ON")
        {
            musicButton.GetComponent<Text>().text = "OFF";

            musicController.GetComponent<AudioSource>().Stop();
            PlayerPrefs.SetInt("isMusicEnabled", 0);
        }
        else if (musicButton.GetComponent<Text>().text == "OFF")
        {
            musicButton.GetComponent<Text>().text = "ON";
            musicController.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("isMusicEnabled", 1);
        }
    }

    public void Back()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        optionsPanel.SetActive(false);
    }
}
