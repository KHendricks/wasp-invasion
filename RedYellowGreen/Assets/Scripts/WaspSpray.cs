using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspSpray : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.gameObject.transform.position.x - 3f,
                                                    player.gameObject.transform.position.y,
                                                    player.gameObject.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
