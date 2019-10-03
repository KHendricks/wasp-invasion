using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    private float startTime;
    private GameObject difficultyText;
    private GameObject enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        difficultyText = GameObject.Find("DifficultyText");
        enemySpawner = GameObject.Find("EnemySpawner");
        difficultyText.SetActive(false);

        StartCoroutine(DifficultyPopupMessage(5,
                                              "Buzz buzz\nDo you hear something?"));
        StartCoroutine(DifficultyPopupMessage(27, 
                                              "Did you know that wasps and toucans\nare mortal enemies?"));
        StartCoroutine(DifficultyPopupMessage(152,
                                              "The wasps are annoyed."));
        StartCoroutine(DifficultyPopupMessage(177,
                                              "Have you been stealing their apples?"));
        StartCoroutine(DifficultyPopupMessage(1102,
                                              "Do you know what else wasps\nhate besides toucans?"));
        StartCoroutine(DifficultyPopupMessage(1127,
                                              "WASPS HATE EVERYTHING..."));
    }

    public void SendPopupText(float delay, string message)
    {
        StartCoroutine(VictoryPopupMessage(delay, message));
    }

    IEnumerator VictoryPopupMessage(float delay, string message)
    {
        yield return new WaitForSeconds(delay);
        difficultyText.SetActive(true);
        StartCoroutine(LerpUp());
        difficultyText.GetComponent<TextMeshProUGUI>().text = message;

        yield return new WaitForSeconds(2.5f);
        difficultyText.transform.localScale = new Vector3(.5f, .5f, .5f);
        difficultyText.GetComponent<TextMeshProUGUI>().text = "";
        difficultyText.SetActive(false);
    }

    IEnumerator DifficultyPopupMessage(float delay, string message)
    {
        yield return new WaitForSeconds(delay);
        enemySpawner.GetComponent<EnemySpawner>().stopSpawning = false;
        difficultyText.SetActive(true);
        StartCoroutine(LerpUp());
        difficultyText.GetComponent<TextMeshProUGUI>().text = message;

        yield return new WaitForSeconds(2.5f);
        enemySpawner.GetComponent<EnemySpawner>().stopSpawning = true;
        difficultyText.transform.localScale = new Vector3(.5f, .5f, .5f);
        difficultyText.GetComponent<TextMeshProUGUI>().text = "";
        difficultyText.SetActive(false);
    }

    IEnumerator LerpUp()
    {
        float progress = 0;
        Vector3 initialScale = difficultyText.transform.localScale;
        Vector3 finalScale = new Vector3(1.2f, 1.2f, 1.2f);
        float timeScale = 3f;

        while (progress <= 1)
        {
            difficultyText.transform.localScale = Vector3.Lerp(initialScale, finalScale, progress);
            progress += Time.deltaTime * timeScale; ;
            yield return null;
        }

        difficultyText.transform.localScale = finalScale;
    }
}
