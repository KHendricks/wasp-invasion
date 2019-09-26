﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        SetHealth(3);
        SetSpeed(-2f);
        Destroy(gameObject, 30f);

        if (!gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            SetSpeed(GetSpeed() * -1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(transform.position.x + (GetSpeed() * Time.deltaTime),
                                         transform.position.y,
                                         transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
