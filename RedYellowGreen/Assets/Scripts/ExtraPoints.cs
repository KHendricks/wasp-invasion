using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraPoints : MonoBehaviour
{
    private GameObject scriptManager;
    private GameObject pickupSound;
    private GameObject enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        pickupSound = GameObject.Find("MunchSound");
        scriptManager = GameObject.Find("ScriptManager");
        enemySpawner = GameObject.Find("EnemySpawner");

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
            PlayerPrefs.SetInt("appleCount", PlayerPrefs.GetInt("appleCount") + 1);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            scriptManager.GetComponent<PointController>().AddPoints(5);
        }
    }

    IEnumerator RemoveFromList()
    {
        yield return new WaitForSeconds(60f);
        enemySpawner.GetComponent<EnemySpawner>().objectLocations.Remove(gameObject.transform.position);
        Destroy(gameObject);
    }
}
