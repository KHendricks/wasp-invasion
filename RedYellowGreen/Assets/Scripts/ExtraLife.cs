using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    private GameObject scriptManager;
    private GameObject pickupSound;
    private GameObject enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        pickupSound = GameObject.Find("BlipSound");
        scriptManager = GameObject.Find("ScriptManager");
        enemySpawner = GameObject.Find("EnemySpawner");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("heartCount", PlayerPrefs.GetInt("heartCount") + 1);
            if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
            {
                pickupSound.GetComponent<AudioSource>().Play();
            }

            collision.gameObject.GetComponent<PlayerControls>().lives = 3;
            scriptManager.GetComponent<PlayerStatusCheck>().UpdateLivesUI();
            gameObject.SetActive(false);
        }
    }

    IEnumerator RemoveFromList()
    {
        yield return new WaitForSeconds(60f);
        enemySpawner.GetComponent<EnemySpawner>().objectLocations.Remove(gameObject.transform.position);
        Destroy(gameObject);
    }
}
