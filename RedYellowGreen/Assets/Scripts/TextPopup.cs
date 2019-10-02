using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    private float startTime;
    private GameObject difficutlyText;
    private GameObject enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        difficutlyText = GameObject.Find("DifficultyText");
        enemySpawner = GameObject.Find("EnemySpawner");
        difficutlyText.SetActive(false);

        StartCoroutine(DifficultyPopupMessage(5,
                                              "Buzz buzz\nDo you hear something?"));
        StartCoroutine(DifficultyPopupMessage(27, 
                                              "Did you know wasps and toucans\nare mortal enemies?"));
        StartCoroutine(DifficultyPopupMessage(52,
                                              "The wasps are annoyed"));
        StartCoroutine(DifficultyPopupMessage(77,
                                              "The wasps are frustrated"));
        StartCoroutine(DifficultyPopupMessage(102,
                                              "Did you know wasps don't hate only toucans?"));
        StartCoroutine(DifficultyPopupMessage(127,
                                              "Wasps hate everyone."));
    }

    IEnumerator DifficultyPopupMessage(float delay, string message)
    {
        yield return new WaitForSeconds(delay);
        enemySpawner.GetComponent<EnemySpawner>().stopSpawning = false;
        difficutlyText.SetActive(true);
        StartCoroutine(LerpUp());
        difficutlyText.GetComponent<TextMeshProUGUI>().text = message;

        yield return new WaitForSeconds(2.5f);
        enemySpawner.GetComponent<EnemySpawner>().stopSpawning = true;
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
