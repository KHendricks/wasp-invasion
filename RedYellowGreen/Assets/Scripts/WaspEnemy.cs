using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }

        SetHealth(3);
        Destroy(gameObject, 30f);
        if (!gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            SetSpeed(GetSpeed() * -1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(transform.position.x + (-1 * (GetSpeed() * Time.deltaTime)),
                                         transform.position.y,
                                         transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
