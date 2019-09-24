using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public int timer;
    public Text timerText;
    public bool timerFinished;

    private GameObject timerSound;

    // Start is called before the first frame update
    void Start()
    {
        timerFinished = false;
        timerSound = GameObject.Find("TimerSound");
        StartCoroutine(CountdownTimer(timer)); 
    }


    IEnumerator CountdownTimer(int timeRemaining)
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            timerSound.GetComponent<AudioSource>().Play();
        }

        timerText.text = timeRemaining.ToString();
        yield return new WaitForSeconds(1);
        if (timeRemaining - 1 > 0)
        {
            StartCoroutine(CountdownTimer(timeRemaining - 1));
        }
        else
        {
            if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
            {
                timerSound.GetComponent<AudioSource>().Play();
                yield return new WaitForSeconds(.1f);

                timerSound.GetComponent<AudioSource>().Play();
                yield return new WaitForSeconds(.1f);

                timerSound.GetComponent<AudioSource>().Play();
                yield return new WaitForSeconds(.1f);
            }

            timerText.text = "";
            timerFinished = true;

        }
    }
}
