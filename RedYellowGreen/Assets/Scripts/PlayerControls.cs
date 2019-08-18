using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Animator anim;
    private float moveSpeed, dirX;
    private float jumpHeight;
    private bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        moveSpeed = 1.5f;
        jumpHeight = 200;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        /*
        if (dirX != 0)
        {
            if (dirX > 0)
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            else if (dirX < 0)
                gameObject.GetComponent<SpriteRenderer>().flipX = true;

            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        */

    }

    void Movement()
    {
        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + dirX,
                                            transform.position.y,
                                            transform.position.z);
    }

    void Jump()
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
