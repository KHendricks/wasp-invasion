﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    public GameObject stopLight;

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
        if (timerFinished)
        {
            if (stopLightReady)
            {
                stopLightReady = false;
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
}