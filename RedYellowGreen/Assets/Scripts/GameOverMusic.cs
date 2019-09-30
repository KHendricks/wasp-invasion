using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMusic : MonoBehaviour
{
    private GameObject musicController;

    // Start is called before the first frame update
    void Start()
    {
        musicController = GameObject.FindWithTag("MusicController");

        if (PlayerPrefs.GetInt("isMusicEnabled") == 1)
        {
            musicController.GetComponent<AudioSource>().Play();
        }
        else if (PlayerPrefs.GetInt("isMusicEnabled") == 0)
        {
            musicController.GetComponent<AudioSource>().Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
