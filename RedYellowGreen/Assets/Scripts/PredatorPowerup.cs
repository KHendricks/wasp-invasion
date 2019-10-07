using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorPowerup : MonoBehaviour
{
    private GameObject player;
    private GameObject scriptManager;

    public bool enablePredatorPowerup;
    private GameObject injuredSound;

    // Start is called before the first frame update
    void Start()
    {
        injuredSound = GameObject.Find("WaspDeath");
        player = GameObject.FindWithTag("Player");
        scriptManager = GameObject.Find("ScriptManager");

        enablePredatorPowerup = false;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.gameObject.transform.position.x + .2f,
                                                    player.gameObject.transform.position.y,
                                                    player.gameObject.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enablePredatorPowerup)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
                {
                    injuredSound.GetComponent<AudioSource>().Play();
                }

                Destroy(collision.gameObject);
                scriptManager.GetComponent<PointController>().AddPoints(10);
            }
        }
    }
}
