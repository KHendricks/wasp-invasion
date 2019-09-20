using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogControls : MonoBehaviour
{
    private GameObject player;
    private float jumpHeight;
    private bool canJump;
    private GameObject[] livesUi;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<Rigidbody2D>().gravityScale = 1f;

        livesUi = new GameObject[3];
        livesUi[0] = GameObject.Find("Lives - 1");
        livesUi[1] = GameObject.Find("Lives - 2");
        livesUi[2] = GameObject.Find("Lives - 3");

        jumpHeight = 200;
        canJump = true;

        // Set sprite
        gameObject.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Sprites/frog_still");

        // Adds collider
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.GetComponent<PolygonCollider2D>().offset =  new Vector2(0, .2f);

        // Set animator
        gameObject.GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load<RuntimeAnimatorController>("Animators/Frog");

        // Set lives UI
        for (int i = 0; i < 3; i++)
        {
            livesUi[i].GetComponent<Image>().sprite =
                Resources.Load<Sprite>("Sprites/frog_still");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Leap();
        }
    }

    public void Leap()
    {
        if (canJump)
        {
            canJump = false;
            gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * jumpHeight);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }

}
