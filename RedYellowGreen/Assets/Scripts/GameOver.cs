using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private GameObject loScoreText, hiScoreText, scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText   = GameObject.Find("scoreText");
        hiScoreText = GameObject.Find("HiScoreText");
        loScoreText = GameObject.Find("LoScoreText");


        scoreText.GetComponent<Text>().text   = scoreText.GetComponent<Text>().text +
                                                PlayerPrefs.GetInt("score").ToString();
        hiScoreText.GetComponent<Text>().text = hiScoreText.GetComponent<Text>().text +
                                                PlayerPrefs.GetInt("hiScore").ToString();
        loScoreText.GetComponent<Text>().text = loScoreText.GetComponent<Text>().text +
                                                PlayerPrefs.GetInt("loScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        Initiate.Fade("LevelOne", Color.black, 1f);
    }

    public void MainMenu()
    {
        Initiate.Fade("MainMenu", Color.black, 1f);
    }
}
