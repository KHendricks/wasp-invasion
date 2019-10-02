using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    private float startTime;
    private GameObject difficutlyText;

    // Start is called before the first frame update
    void Start()
    {
        difficutlyText = GameObject.Find("DifficultyText");
        difficutlyText.SetActive(false);

        StartCoroutine(DifficultyPopupMessage(20, 
                                              "The wasps are irritated"));
        StartCoroutine(DifficultyPopupMessage(40,
                                              "The wasps are annoyed"));
        StartCoroutine(DifficultyPopupMessage(60,
                                              "The wasps are frustrated"));
        StartCoroutine(DifficultyPopupMessage(80,
                                              "The wasps are angry"));
        StartCoroutine(DifficultyPopupMessage(100,
                                              "The wasps are furious"));
    }

    IEnumerator DifficultyPopupMessage(float delay, string message)
    {
        yield return new WaitForSeconds(delay);
        difficutlyText.SetActive(true);
        StartCoroutine(LerpUp());
        difficutlyText.GetComponent<TextMeshProUGUI>().text = message;

        yield return new WaitForSeconds(2f);
        difficutlyText.transform.localScale = new Vector3(.5f, .5f, .5f);
        difficutlyText.GetComponent<TextMeshProUGUI>().text = "";
        difficutlyText.SetActive(false);
    }

    IEnumerator LerpUp()
    {
        float progress = 0;
        Vector3 initialScale = difficutlyText.transform.localScale;
        Vector3 finalScale = new Vector3(1.2f, 1.2f, 1.2f);
        float timeScale = 3f;

        while (progress <= 1)
        {
            difficutlyText.transform.localScale = Vector3.Lerp(initialScale, finalScale, progress);
            progress += Time.deltaTime * timeScale; ;
            yield return null;
        }

        difficutlyText.transform.localScale = finalScale;
    }
}
