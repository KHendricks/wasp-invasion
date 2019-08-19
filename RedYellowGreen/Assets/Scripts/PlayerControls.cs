using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Animator anim;
    private float playerSpeed, runSpeed, walkSpeed, dirX;
    private float jumpHeight;
    private bool canJump;
    private GameObject scriptManager;

    public string movementState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        scriptManager = GameObject.Find("ScriptManager");

        playerSpeed = 0;
        runSpeed = 1.5f;
        walkSpeed = .5f;
        jumpHeight = 200;

        movementState = "Stopped";
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptManager.GetComponent<Countdown>().timerFinished)
        {
            Movement();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Movement()
    {
        dirX = playerSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + dirX,
                                            transform.position.y,
                                            transform.position.z);
    }

    public void WalkRight()
    {
        movementState = "Walking";
        playerSpeed = walkSpeed;
    }

    public void WalkLeft()
    {
        movementState = "Walking";
        playerSpeed = -walkSpeed;
    }

    public void RunRight()
    {
        movementState = "Running";
        playerSpeed = runSpeed;
    }

    public void RunLeft()
    {
        movementState = "Running";
        playerSpeed = -runSpeed;
    }

    public void StopMoving()
    {
        movementState = "Stopped";
        playerSpeed = 0;
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
