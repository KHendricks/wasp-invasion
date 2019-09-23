using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : Enemy
{
    private GameObject player;
    private Vector3 endPosition;
    private float xOffset, yOffset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        SetSpeed(3f);
        DetermineOffset();
        endPosition = new Vector3(player.transform.position.x + this.xOffset,
                                  player.transform.position.y + this.yOffset,
                                  player.transform.position.z);
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                                                            this.endPosition,
                                                            GetSpeed() * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

    void DetermineOffset()
    {
        yOffset = -1f;

        // X Offset
        if (player.GetComponent<PlayerControls>().GetMovementState() == "Running")
        {
            // Moving Left
            if (player.GetComponent<SpriteRenderer>().flipX)
            {
                this.xOffset = 2.5f;
            }
            else
            {
                this.xOffset = 2.5f;
            }
        }
        else if (player.GetComponent<PlayerControls>().GetMovementState() == "Walking")
        {
            // Moving Left
            if (player.GetComponent<SpriteRenderer>().flipX)
            {
                this.xOffset = -1;
            }
            else
            {
                this.xOffset = 1;
            }
        }
    }
}
