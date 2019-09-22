using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{
    private GameObject player;
    private int playerScore;
    private bool pointAddDelayRunning;
    private GameObject pointText;
    private bool enablePointsRunning;
    private bool enableAddPoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pointText = GameObject.Find("PointText");
        playerScore = 0;
        pointText.GetComponent<Text>().text = playerScore.ToString();

        pointText.SetActive(true);
        pointAddDelayRunning = false;
        enablePointsRunning = false;
        enableAddPoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Countdown>().timerFinished)
        {
            AddPoint();
        }
    }

    public void AddPoint()
    {
        string lightState = gameObject.GetComponent<LightController>().GetLightState();
        string playerState = player.GetComponent<PlayerControls>().GetMovementState();

        if (!pointAddDelayRunning && enableAddPoint)
        {
            StartCoroutine(AddPointDelay(.5f, lightState, playerState));
        }
    }

    IEnumerator AddPointDelay(float timeDelay, string lightState, string playerState)
    {
        pointAddDelayRunning = true;
        yield return new WaitForSeconds(timeDelay);

        if ((lightState == "Red" && playerState == "Stopped") ||
            (lightState == "Green" && playerState == "Running") || 
            (lightState == "Yellow" && playerState == "Walking"))
        {
            playerScore += 1;
            pointText.GetComponent<Text>().text = playerScore.ToString();
        }
        else
        {
            playerScore -= 1;
            pointText.GetComponent<Text>().text = playerScore.ToString();
        }

        pointAddDelayRunning = false;
    }

    public void EnablePoints()
    {
        if (!enablePointsRunning)
        {
            StartCoroutine(EnablePoints(1f));
        }
    }

    IEnumerator EnablePoints(float timer)
    {
        enablePointsRunning = true;
        yield return new WaitForSeconds(timer);
        enablePointsRunning = false;
        enableAddPoint = true;
    }

    public void SetEnableAddPoint(bool val)
    {
        enableAddPoint = val;
    }

    public bool GetEnableAddPoint()
    {
        return enableAddPoint;
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }
}
