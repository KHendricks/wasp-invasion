using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    private Animator anim;
    private float playerSpeed, runSpeed, walkSpeed, dirX;
    private float jumpHeight;
    private bool canJump;
    private GameObject scriptManager;
    private Animator playerAnimator;
    private float[] flightLevels;
    private int currentFlightIndex;
    private bool isLerping;

    public GameObject[] playerLives;
    public int lives;
    public string movementState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        scriptManager = GameObject.Find("ScriptManager");
        playerAnimator = gameObject.GetComponent<Animator>();
        playerLives = new GameObject[3];
        playerLives[0] = GameObject.Find("Lives - 1");
        playerLives[1] = GameObject.Find("Lives - 2");
        playerLives[2] = GameObject.Find("Lives - 3");

        playerSpeed = 0;
        runSpeed = 1.5f;
        walkSpeed = .5f;
        jumpHeight = 200;
        lives = 3;

        currentFlightIndex = 0;
        flightLevels = new float[3];
        flightLevels[0] = -.5f;
        flightLevels[1] = -.2f;
        flightLevels[2] = .2f;

        movementState = "Stopped";
        canJump = true;
        isLerping = false;
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
            FlyUp();
        }

        if (isLerping)
        {

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

        // Sets player animation to walk right
        playerAnimator.SetBool("isSlowFlying", true);
        playerAnimator.SetBool("isFastFlying", false);
        playerAnimator.SetBool("isIdle", false);
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void WalkLeft()
    {
        movementState = "Walking";
        playerSpeed = -walkSpeed;
        playerAnimator.SetBool("isSlowFlying", true);
        playerAnimator.SetBool("isFastFlying", false);
        playerAnimator.SetBool("isIdle", false);
        if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void RunRight()
    {
        movementState = "Running";
        playerSpeed = runSpeed;

        playerAnimator.SetBool("isSlowFlying", false);
        playerAnimator.SetBool("isFastFlying", true);
        playerAnimator.SetBool("isIdle", false);
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void RunLeft()
    {
        movementState = "Running";
        playerSpeed = -runSpeed;

        playerAnimator.SetBool("isSlowFlying", false);
        playerAnimator.SetBool("isFastFlying", true);
        playerAnimator.SetBool("isIdle", false);
        if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void StopMoving()
    {
        movementState = "Stopped";
        playerSpeed = 0;

        playerAnimator.SetBool("isSlowFlying", false);
        playerAnimator.SetBool("isFastFlying", false);
        playerAnimator.SetBool("isIdle", true);
    }

    public void FlyUp()
    {
        Debug.Log(currentFlightIndex);
        if (currentFlightIndex < 2 )
        {
            currentFlightIndex++;
        }

        Vector3 newPosition = new Vector3(gameObject.transform.position.x,
                                            flightLevels[currentFlightIndex],
                                            gameObject.transform.position.z);

        StartCoroutine(FlyUpMovement(newPosition, 1f));
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPosition, Time.deltaTime * 2);
    }

    IEnumerator FlyUpMovement(Vector3 newPosition, float delay)
    {
        yield return null;
    }

    public void OldJump()
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

    public string GetMovementState()
    {
        return movementState;
    }
}
