using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    public GameObject tipsPanel;

    // Start is called before the first frame update
    void Start()
    {
        tipsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayTipsPanel()
    {
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
