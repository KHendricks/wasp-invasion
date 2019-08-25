using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusCheck : MonoBehaviour
{
    private bool checkStatusRunning, injuredTimerRunning;
    private GameObject player;
    private Button walkLeft, walkRight, runLeft, runRight, stop;
    private enum StopLightColors { red = 0, green, yellow };

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        walkLeft = GameObject.Find("Walk - Left").GetComponent<Button>();
        walkRight = GameObject.Find("Walk - Right").GetComponent<Button>();
        runLeft = GameObject.Find("Run - Left").GetComponent<Button>();
        runRight = GameObject.Find("Run - Right").GetComponent<Button>();
        stop = GameObject.Find("Stop Moving").GetComponent<Button>();

        injuredTimerRunning = false;
        checkStatusRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("A");
        if (gameObject.GetComponent<Countdown>().timerFinished)
        {
            Debug.Log("B");
            if (gameObject.GetComponent<LightController>().GetStopLightReadyStatus())
            {
                Debug.Log("C");

                if (!checkStatusRunning)
                {
                    StartCoroutine(CheckStatus(1f));
                }
            }
        }
    }

    void CheckPlayerStatus()
    {
        Debug.Log(gameObject.GetComponent<LightController>().GetCurrentIndex());
        Debug.Log((int)StopLightColors.red + " " + (int)StopLightColors.yellow + " " + (int)StopLightColors.green);
        // Red Light - Player must be stopped
        if (gameObject.GetComponent<LightController>().GetCurrentIndex() == (int)StopLightColors.red)
        {
            //DisableMovementButtonInteraction(gameObject.GetComponent<LightController>().GetCurrentIndex());
            if (player.GetComponent<PlayerControls>().movementState != "Stopped")
            {
                player.GetComponent<PlayerControls>().lives -= 1;
                UpdateLivesUI();
                InjuredFlash();
            }
        }
        // Green Light - Player must be Running 
        else if (gameObject.GetComponent<LightController>().GetCurrentIndex() == (int)StopLightColors.green)
        {
            //DisableMovementButtonInteraction(gameObject.GetComponent<LightController>().GetCurrentIndex());
            if (player.GetComponent<PlayerControls>().movementState != "Running")
            {
                player.GetComponent<PlayerControls>().lives -= 1;
                UpdateLivesUI();
                InjuredFlash();
            }
        }
        // Yellow Light - Player must be Walking
        else if (gameObject.GetComponent<LightController>().GetCurrentIndex() == (int)StopLightColors.yellow)
        {
            //DisableMovementButtonInteraction(gameObject.GetComponent<LightController>().GetCurrentIndex());
            if (player.GetComponent<PlayerControls>().movementState != "Walking")
            {
                player.GetComponent<PlayerControls>().lives -= 1;
                UpdateLivesUI();
                InjuredFlash();
            }
        }

        if (player.GetComponent<PlayerControls>().lives < 1)
        {
            Initiate.Fade("GameOver", Color.black, 1f);
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

    void DisableMovementButtonInteraction(int lightColor)
    {
        switch (lightColor)
        {
            case (int)StopLightColors.red:
                walkLeft.interactable = false;
                walkRight.interactable = false;
                runLeft.interactable = false;
                runRight.interactable = false;
                stop.interactable = true;
                break;

            case (int)StopLightColors.yellow:
                walkLeft.interactable = true;
                walkRight.interactable = true;
                runLeft.interactable = false;
                runRight.interactable = false;
                stop.interactable = false;
                break;

            case (int)StopLightColors.green:
                walkLeft.interactable = false;
                walkRight.interactable = false;
                runLeft.interactable = true;
                runRight.interactable = true;
                stop.interactable = false;
                break;

            default:
                break;
        }

    }

    IEnumerator CheckStatus(float timer)
    {
        checkStatusRunning = true;
        yield return new WaitForSeconds(timer);
        checkStatusRunning = false;
        CheckPlayerStatus();
    }

}
