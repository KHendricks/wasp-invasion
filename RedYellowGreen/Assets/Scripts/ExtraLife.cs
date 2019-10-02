using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    private GameObject scriptManager;
    private GameObject pickupSound;

    // Start is called before the first frame update
    void Start()
    {
        pickupSound = GameObject.Find("BlipSound");
        scriptManager = GameObject.Find("ScriptManager");
        Destroy(gameObject, 60);
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

            collision.gameObject.GetComponent<PlayerControls>().lives = 3;
            scriptManager.GetComponent<PlayerStatusCheck>().UpdateLivesUI();
            gameObject.SetActive(false);
        }
    }
}
