using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    private GameObject stopLight, player;
    private bool timerFinished, stopLightReady;
    private int currentIndex;
    private Color32[] colorList = {
            new Color32(255, 0, 0, 255),    // Red
            new Color32(0, 255, 0, 255),    // Green
            new Color32(255, 255, 0, 255)   // Yellow
        };
        
    // Start is called before the first frame update
    void Start()
    {
        stopLight = GameObject.Find("Stoplight");
        player = GameObject.Find("Player");

        stopLight.SetActive(false);
        int timer = gameObject.GetComponent<Countdown>().timer;
        timerFinished = gameObject.GetComponent<Countdown>().timerFinished;
        StartCoroutine(TimerDelay(timer));

        // Initial Stoplight color
        stopLightReady = true;
        currentIndex = 0;
        stopLight.GetComponent<Image>().color = colorList[currentIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Countdown>().timerFinished)
        {
            if (stopLightReady)
            {
                StartCoroutine(StoplightDelay(Random.Range(3, 8)));
                ChangeLight();
            }
        }
    }

    IEnumerator TimerDelay(int timeRemaining)
    {
        yield return new WaitForSeconds(timeRemaining + .5f);
        stopLight.SetActive(true);
        timerFinished = gameObject.GetComponent<Countdown>().timerFinished;
    }

    IEnumerator StoplightDelay(int timeRemaining)
    {
        stopLightReady = false;
        yield return new WaitForSeconds(timeRemaining);
        stopLightReady = true;
    }

    void ChangeLight()
    {
        int newLightIndex;

        do
        {
            newLightIndex = Random.Range(0, colorList.Length);
        } while (currentIndex == newLightIndex);

        currentIndex = newLightIndex;
        stopLight.GetComponent<Image>().color = colorList[currentIndex];
    }

    public bool GetStopLightReadyStatus()
    {
        return stopLightReady;
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
