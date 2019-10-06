using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopSpawningPowerup : MonoBehaviour
{
    private GameObject scriptManager;
    private GameObject pickupSound;
    private GameObject enemySpawner;
    private GameObject icyTint;

    // Start is called before the first frame update
    void Start()
    {
        pickupSound = GameObject.Find("IceCrunch");
        scriptManager = GameObject.Find("ScriptManager");
        enemySpawner = GameObject.Find("EnemySpawner");
        icyTint = GameObject.Find("IcyTint");

        StartCoroutine(RemoveFromList());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
            {
                pickupSound.GetComponent<AudioSource>().Play();
            }
            StartCoroutine(BeginPowerUp());
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
    }

    IEnumerator RemoveFromList()
    {
        yield return new WaitForSeconds(60f);
        enemySpawner.GetComponent<EnemySpawner>().objectLocations.Remove(gameObject.transform.position);
        Destroy(gameObject);
    }

    IEnumerator BeginPowerUp()
    {
        Debug.Log("Stop Spawning");
        enemySpawner.GetComponent<EnemySpawner>().stopSpawning = true;
        icyTint.GetComponent<Image>().color = new Color32(105, 233, 255, 155);
        yield return new WaitForSeconds(10f);
        icyTint.GetComponent<Image>().color = new Color32(105, 233, 255, 0);
        Debug.Log("Start Spawning");
        enemySpawner.GetComponent<EnemySpawner>().stopSpawning = false;
    }
}
