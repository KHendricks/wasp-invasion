using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    public GameObject tipsPanel;
    private GameObject buttonPressSound;
    public GameObject[] descriptionText;

    private GameObject nextButton;
    private GameObject prevButton;

    // Start is called before the first frame update
    void Start()
    {
        buttonPressSound = GameObject.Find("ButtonPress");

        descriptionText = new GameObject[2];
        descriptionText[0] = GameObject.Find("DescriptionText");
        descriptionText[1] = GameObject.Find("DescriptionTextPg2");
        nextButton = GameObject.Find("NextButton");
        prevButton = GameObject.Find("PrevButton");

        descriptionText[1].SetActive(false);
        prevButton.SetActive(false);

        tipsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayTipsPanel()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        if (tipsPanel.activeSelf)
        {
            tipsPanel.SetActive(false);
        }
        else
        {
            tipsPanel.SetActive(true);
        }
    }

    public void NextPage()
    {
        descriptionText[0].SetActive(false);
        descriptionText[1].SetActive(true);

        nextButton.SetActive(false);
        prevButton.SetActive(true);
    }

    public void PrevPage()
    {
        descriptionText[0].SetActive(true);
        descriptionText[1].SetActive(false);

        nextButton.SetActive(true);
        prevButton.SetActive(false);
    }
}
