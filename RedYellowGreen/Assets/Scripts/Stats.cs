using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public GameObject statsPanel;
    private GameObject buttonPressSound;

    public Text[] statsText;
    // Start is called before the first frame update
    void Start()
    {
        buttonPressSound = GameObject.Find("ButtonPress");
        SetDisplays();

        statsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayStatsPanel()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        if (statsPanel.activeSelf)
        {
            statsPanel.SetActive(false);
        }
        else
        {
            statsPanel.SetActive(true);
        }
    }

    void SetDisplays()
    {
        statsText[0].text = PlayerPrefs.GetInt("hiScore").ToString();
        statsText[1].text = PlayerPrefs.GetInt("loScore").ToString();

        System.TimeSpan time = System.TimeSpan.FromSeconds(PlayerPrefs.GetInt("timePlayed"));
        statsText[2].text = time.ToString(@"hh\:mm\:ss");

        statsText[3].text = PlayerPrefs.GetInt("appleCount").ToString();
        statsText[4].text = PlayerPrefs.GetInt("heartCount").ToString();

    }
}
