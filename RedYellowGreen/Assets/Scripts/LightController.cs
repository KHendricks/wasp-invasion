using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    private GameObject stopLight, player;
    private bool timerFinished, stopLightReady;
    private int currentIndex;
    private bool checkStatusRunning, injuredTimerRunning;

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

        injuredTimerRunning = false;

        // Initial Stoplight color
        stopLightReady = true;
        currentIndex = 0;
        stopLight.GetComponent<Image>().color = colorList[currentIndex];checkStatusRunning = false;
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

                if (!checkStatusRunning)
                {
                    StartCoroutine(CheckStatus(1f));
                }
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

    void CheckPlayerStatus()
    {
        // Red Light - Player must be stopped
        if (currentIndex == 0)
        {
            if (player.GetComponent<PlayerControls>().movementState != "Stopped")
            {
                player.GetComponent<PlayerControls>().lives -= 1;
                UpdateLivesUI();
                InjuredFlash();
            }
        }
        // Green Light - Player must be Running 
        else if (currentIndex == 1)
        {
            if (player.GetComponent<PlayerControls>().movementState != "Running")
            {
                player.GetComponent<PlayerControls>().lives -= 1;
                UpdateLivesUI();
                InjuredFlash();
                ResetLight();
            }
        }
        // Yellow Light - Player must be Walking
        else if (currentIndex == 2)
        {
            if (player.GetComponent<PlayerControls>().movementState != "Walking")
            {
                player.GetComponent<PlayerControls>().lives -= 1;
                UpdateLivesUI();
                InjuredFlash();
            }
        }

        if (player.GetComponent<PlayerControls>().lives < 1)
        {
            Debug.Log("Game Over");
        }
    }

    void UpdateLivesUI()
    {
        if (player.GetComponent<PlayerControls>().lives == 3)
        {
            player.GetComponent<PlayerControls>().playerLives[2].SetActive(true);
            player.GetComponent<PlayerControls>().playerLives[1].SetActive(true);
            player.GetComponent<PlayerControls>().playerLives[0].SetActive(true);
        }
        else if (player.GetComponent<PlayerControls>().lives == 2)
        {
            player.GetComponent<PlayerControls>().playerLives[2].SetActive(false);
            player.GetComponent<PlayerControls>().playerLives[1].SetActive(true);
            player.GetComponent<PlayerControls>().playerLives[0].SetActive(true);
        }
        else if (player.GetComponent<PlayerControls>().lives == 1)
        {
            player.GetComponent<PlayerControls>().playerLives[2].SetActive(false);
            player.GetComponent<PlayerControls>().playerLives[1].SetActive(false);
            player.GetComponent<PlayerControls>().playerLives[0].SetActive(true);
        }
        else if (player.GetComponent<PlayerControls>().lives == 0)
        {
            player.GetComponent<PlayerControls>().playerLives[2].SetActive(false);
            player.GetComponent<PlayerControls>().playerLives[1].SetActive(false);
            player.GetComponent<PlayerControls>().playerLives[0].SetActive(false);
        }
    }

    void InjuredFlash()
    {
        if (!injuredTimerRunning)
        {
            StartCoroutine(InjuredFlashTimer());
        }
    }

    void ResetLight()
    {
        // Need to properly keep track of light if player changes
        // movement state.
        // Need to reset properly when player misses a light.

        //StopCoroutine("StoplightDelay");
        //StartCoroutine(ResetLightDelay(1.5f));
    }

    IEnumerator ResetLightDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        stopLightReady = true;
    }

    IEnumerator InjuredFlashTimer()
    {
        injuredTimerRunning = true;
        player.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.2f);
        player.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(.2f);
        player.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.2f);
        player.GetComponent<SpriteRenderer>().color = Color.white;
        injuredTimerRunning = false;
    }

    IEnumerator CheckStatus(float timer)
    {
        checkStatusRunning = true;
        yield return new WaitForSeconds(timer);
        checkStatusRunning = false;
        CheckPlayerStatus();
    }
}
