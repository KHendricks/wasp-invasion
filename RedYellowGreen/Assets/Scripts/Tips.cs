using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    public GameObject tipsPanel;
    private GameObject buttonPressSound;

    // Start is called before the first frame update
    void Start()
    {
        tipsPanel.SetActive(false);
        buttonPressSound = GameObject.Find("ButtonPress");
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
}
