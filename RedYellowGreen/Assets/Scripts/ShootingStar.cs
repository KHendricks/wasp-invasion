using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : Enemy
{
    private GameObject player;
    private float startTime;
    private float totalDistance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        startTime = Time.time;
        totalDistance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        SetSpeed(.5f);
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceCovered = (Time.time - startTime) * GetSpeed();
        float fractionOfJourney = distanceCovered / totalDistance;

        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,
                                                     player.transform.position,
                                                     fractionOfJourney);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
