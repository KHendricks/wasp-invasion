using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraPoints : MonoBehaviour
{
    private GameObject scriptManager;
    private GameObject pickupSound;

    // Start is called before the first frame update
    void Start()
    {
        pickupSound = GameObject.Find("MunchSound");
        scriptManager = GameObject.Find("ScriptManager");
        Destroy(gameObject, 20);
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

            gameObject.SetActive(false);
            scriptManager.GetComponent<PointController>().AddPoints(20);
        }
    }
}
