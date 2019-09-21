using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private GameObject soundButton;
    public GameObject optionsPanel;


    // Start is called before the first frame update
    void Start()
    {
        soundButton = GameObject.Find("SoundButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundUpdate()
    {
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

    public void Back()
    {
        optionsPanel.SetActive(false);
    }
}
