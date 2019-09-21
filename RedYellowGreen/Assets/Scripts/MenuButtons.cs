using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameObject optionsPanel, playButton;
    private Rigidbody playButtonRb;
    private bool currentlyWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("isSoundEnabled"))
        {
            PlayerPrefs.SetInt("isSoundEnabled", 1);
        }

        optionsPanel.SetActive(false);
        playButton = GameObject.Find("PlayButton");
        playButtonRb = playButton.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Button is defaulted to:
    // is kinematic = true
    // use gravity = false
    public void PlayButtonPress()
    {
        // Values are arbitrary. Sometimes the button can move really slowly or
        // very quickly. 
        Vector3 playButtonVel = new Vector3(Random.Range(-1000, 1000), Random.Range(-1200, 1200), 0);
        playButtonRb.isKinematic = false;
        playButtonRb.velocity = playButtonVel;

        // This is so the button can't be pressed again and change the direction
        playButton.GetComponent<Button>().interactable = false;

        ChangeSceneDelay(.5f);
    }

    void ChangeSceneDelay(float x)
    {
        if (!currentlyWaiting)
        {
            StartCoroutine(ChangeSceneDelayHelper(x));
        }
    }

    IEnumerator ChangeSceneDelayHelper(float x)
    {
        currentlyWaiting = true;
        yield return new WaitForSeconds(x);
        currentlyWaiting = false;
        Initiate.Fade("LevelOne", Color.black, 1f);
    }

    public void OptionsButton()
    {
        if (optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(false);
        }
        else
        {
            optionsPanel.SetActive(true);
        }
    }
}
