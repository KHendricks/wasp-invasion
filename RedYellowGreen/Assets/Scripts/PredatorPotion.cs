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

    // Start is called before the first frame update
    void Start()
    {
        pickupSound = GameObject.Find("Slurp");
        scriptManager = GameObject.Find("ScriptManager");
        enemySpawner = GameObject.Find("EnemySpawner");
        predatorPowerup = GameObject.Find("PredatorPotion");

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
            StartCoroutine(BeginPowerUp(collision.gameObject));
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
    }

    IEnumerator RemoveFromList()
    {
        yield return new WaitForSeconds(60f);
        enemySpawner.GetComponent<EnemySpawner>().objectLocations.Remove(gameObject.transform.position);
        Destroy(gameObject);
    }

    IEnumerator BeginPowerUp(GameObject player)
    {
        predatorPowerup.GetComponent<PredatorPowerup>().enablePredatorPowerup = true;

        if (player.GetComponent<PlayerControls>().GetIsInjured())
        {
            player.GetComponent<PlayerControls>().SetIsInjured(false);
            StopCoroutine("InjuredFlash");
        }

        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<SpriteRenderer>().color = new Color32(73, 182, 117, 255);
        yield return new WaitForSeconds(7f);

        for (int i = 0; i < 3; i++)
        {
            player.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.5f);
            player.GetComponent<SpriteRenderer>().color = new Color32(73, 182, 117, 255);
            yield return new WaitForSeconds(.5f);
        }

        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<SpriteRenderer>().color = Color.white;
        predatorPowerup.GetComponent<PredatorPowerup>().enablePredatorPowerup = false;
    }
}
