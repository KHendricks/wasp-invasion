using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    private Animator anim;
    private float Speed, runSpeed, walkSpeed, dirX;
    private GameObject scriptManager;
    private Animator Animator;
    private bool isLerping;
    private bool isInjured;
    private GameObject playerShield;
    private GameObject buttonPressSound;
    private GameObject injuredSound;

    public GameObject[] playerLives;
    public int lives;
    public string prevMovementState, movementState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        scriptManager = GameObject.Find("ScriptManager");
        Animator = gameObject.GetComponent<Animator>();
        playerLives = new GameObject[3];
        playerLives[0] = GameObject.Find("Lives - 1");
        playerLives[1] = GameObject.Find("Lives - 2");
        playerLives[2] = GameObject.Find("Lives - 3");

        Speed = 0;
        runSpeed = 1.5f;
        walkSpeed = .5f;
        lives = 3;

        prevMovementState = "";
        movementState = "Stopped";
        isLerping = false;
        isInjured = false;

        buttonPressSound = GameObject.Find("ButtonPress");
        injuredSound = GameObject.Find("InjuredSound");
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptManager.GetComponent<Countdown>().timerFinished)
        {
            if (SwipeManager.IsSwipingUp())
            {
                gameObject.GetComponent<ToucanControls>().FlyUp();

                // This is to handle the stop button
                if (prevMovementState == "Running")
                    RunRight();
                else if (prevMovementState == "Walking")
                    WalkRight();
                else
                    StopMoving();
            }
            else if (SwipeManager.IsSwipingDown())
            {
                gameObject.GetComponent<ToucanControls>().FlyDown();

                // This is to handle the stop button
                if (prevMovementState == "Running")
                    RunRight();
                else if (prevMovementState == "Walking")
                    WalkRight();
                else
                    StopMoving();
            }
            if (SwipeManager.IsSwipingLeft())
            {
                WalkRight();
            }
            else if (SwipeManager.IsSwipingRight())
            {
                RunRight();
            }
            Movement();
        }
    }

    public void Movement()
    {
        dirX = Speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + dirX,
                                            transform.position.y,
                                            transform.position.z);
    }

    public void WalkRight()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }
        prevMovementState = movementState;
        movementState = "Walking";
        Speed = walkSpeed;

        // Sets  animation to walk right
        Animator.SetBool("isWalking", true);
        Animator.SetBool("isRunning", false);
        Animator.SetBool("isIdle", false);
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void WalkLeft()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        movementState = "Walking";
        Speed = -walkSpeed;
        Animator.SetBool("isWalking", true);
        Animator.SetBool("isRunning", false);
        Animator.SetBool("isIdle", false);
        if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void UpdateMovement()
    {
        if (movementState == "Running")
        {
            WalkRight();
        }
        else if (movementState == "Walking")
        {
            RunRight();
        }
        else
        {
            RunRight();
        }
    }



    public void RunRight()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        prevMovementState = movementState;
        movementState = "Running";
        Speed = runSpeed;

        Animator.SetBool("isWalking", false);
        Animator.SetBool("isRunning", true);
        Animator.SetBool("isIdle", false);
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void RunLeft()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        movementState = "Running";
        Speed = -runSpeed;

        Animator.SetBool("isWalking", false);
        Animator.SetBool("isRunning", true);
        Animator.SetBool("isIdle", false);
        if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void StopMoving()
    {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
        {
            buttonPressSound.GetComponent<AudioSource>().Play();
        }

        prevMovementState = movementState;
        movementState = "Stopped";
        Speed = 0;

        Animator.SetBool("isWalking", false);
        Animator.SetBool("isRunning", false);
        Animator.SetBool("isIdle", true);
    }

    public string GetMovementState()
    {
        return movementState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInjured)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (PlayerPrefs.GetInt("isSoundEnabled") == 1)
                {
                    injuredSound.GetComponent<AudioSource>().Play();
                }

                lives -= 1;
                scriptManager.GetComponent<PlayerStatusCheck>().UpdateLivesUI();
                StartCoroutine(InjuredFlash());
            }
        }

        if (collision.gameObject.tag == "Powerup")
        {
            if (collision.gameObject.name.Contains("Shield"))
            {
                playerShield.SetActive(true);
            }
        }

    }

    IEnumerator InjuredFlash()
    {
        isInjured = true;

        yield return new WaitForSeconds(.25f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.25f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        isInjured = false;
    }

    public float GetRunSpeed()
    {
        return runSpeed;
    }
}
