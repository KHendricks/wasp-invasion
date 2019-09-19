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

    public GameObject[] playerLives;
    public int lives;
    public string movementState;

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

        movementState = "Stopped";
        isLerping = false;

        // Decide what movement, animations, and sprites to use based
        // on selected choice
        if (scriptManager.GetComponent<Preferences>().GetCharacterSelected() == "Toucan")
        {
            gameObject.AddComponent<ToucanControls>();
        }
        else if (scriptManager.GetComponent<Preferences>().GetCharacterSelected() == "Frog")
        {
            gameObject.AddComponent<FrogControls>();
        }
        else if (scriptManager.GetComponent<Preferences>().GetCharacterSelected() == "Wasp")
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptManager.GetComponent<Countdown>().timerFinished)
        {
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

    public void RunRight()
    {
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
}
