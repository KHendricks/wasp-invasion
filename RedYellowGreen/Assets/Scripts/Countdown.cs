using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public int timer;
    public Text timerText;
    public bool timerFinished;

    // Start is called before the first frame update
    void Start()
    {
        timerFinished = false;
        StartCoroutine(CountdownTimer(timer)); 
    }


    IEnumerator CountdownTimer(int timeRemaining)
    {
        timerText.text = timeRemaining.ToString();
        yield return new WaitForSeconds(1);
        if (timeRemaining - 1 > 0)
        {
            StartCoroutine(CountdownTimer(timeRemaining - 1));
        }
        else
        {
            timerText.text = "";
            timerFinished = true;
        }
    }
}
