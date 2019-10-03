using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    private GameObject enemySpawner;
    private GameObject scriptManager;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner");
        enemySpawner.GetComponent<EnemySpawner>().stopSpawning = false;

        scriptManager = GameObject.Find("ScriptManager");
        scriptManager.GetComponent<TextPopup>().SendPopupText(3f,
                                                              "Wow... you actually made it");

        scriptManager.GetComponent<TextPopup>().SendPopupText(6f,
                                                              "I did not think anyone would win");
        scriptManager.GetComponent<TextPopup>().SendPopupText(10f,
                                                              "YOU WIN");

        StartCoroutine(VictoryScreech());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Initiate.Fade("LevelOne", Color.black, 1f);
        }
    }

    IEnumerator VictoryScreech()
    {
        yield return new WaitForSeconds(12f);
        Initiate.Fade("LevelOne", Color.black, 5f * Time.deltaTime);
    }
}
