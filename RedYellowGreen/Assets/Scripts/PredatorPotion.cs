using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PredatorPotion : MonoBehaviour
{
    private GameObject scriptManager;
    private GameObject pickupSound;
    private GameObject enemySpawner;
    private GameObject predatorPowerup;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        pickupSound = GameObject.Find("Slurp");
        scriptManager = GameObject.Find("ScriptManager");
        enemySpawner = GameObject.Find("EnemySpawner");
        predatorPowerup = GameObject.Find("PredatorPotion");
        player = GameObject.FindWithTag("Player");

        StartCoroutine(RemoveFromList());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GreenPowerup")
        {
            if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
            {
                pickupSound.GetComponent<AudioSource>().Play();
            }
            StartCoroutine(BeginPowerUp());
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
        predatorPowerup.GetComponent<PredatorPowerup>().enablePredatorPowerup = true;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<SpriteRenderer>().color = new Color32(73, 182, 117, 255);

        if (player.GetComponent<PlayerControls>().GetIsInjured())
        {
            player.GetComponent<PlayerControls>().SetIsInjured(false);
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            StopCoroutine("InjuredFlash");
        }
        yield return new WaitForSeconds(1f);

        player.GetComponent<SpriteRenderer>().color = new Color32(73, 182, 117, 255);
        yield return new WaitForSeconds(6.5f);

        for (int i = 0; i < 3; i++)
        {
            player.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.5f);
            player.GetComponent<SpriteRenderer>().color = new Color32(73, 182, 117, 255);
            yield return new WaitForSeconds(.5f);
        }

        player.GetComponent<SpriteRenderer>().color = Color.white;
        player.GetComponent<BoxCollider2D>().enabled = true;
        predatorPowerup.GetComponent<PredatorPowerup>().enablePredatorPowerup = false;
    }
}
