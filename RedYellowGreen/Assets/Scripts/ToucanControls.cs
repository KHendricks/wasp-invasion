using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToucanControls : MonoBehaviour
{
    private GameObject player;
    private GameObject[] livesUi;
    private float[] flightLevels;
    private int currentFlightIndex;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;

        livesUi = new GameObject[3];
        livesUi[0] = GameObject.Find("Lives - 1");
        livesUi[1] = GameObject.Find("Lives - 2");
        livesUi[2] = GameObject.Find("Lives - 3");

        // Set flight heights
        currentFlightIndex = 0;
        flightLevels = new float[3];
        flightLevels[0] = -.5f;
        flightLevels[1] = -.2f;
        flightLevels[2] = .2f;

        // Set sprite
        gameObject.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Sprites/toucan_base");

        // Adds collider
        gameObject.AddComponent<PolygonCollider2D>();

        // Set animator
        gameObject.GetComponent<Animator>().runtimeAnimatorController = 
            Resources.Load<RuntimeAnimatorController>("Animators/Toucan");

        // Set lives UI
        for (int i = 0; i < 3; i++)
        {
            livesUi[i].GetComponent<Image>().sprite =
                Resources.Load<Sprite>("Sprites/toucan_still");
        }
    }   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            FlyUp();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FlyDown();
        }
    }

    public void FlyUp()
    {
        if (currentFlightIndex < 2)
        {
            currentFlightIndex++;
        }

        Vector3 newPosition = new Vector3(player.transform.position.x,
                                            flightLevels[currentFlightIndex],
                                            player.transform.position.z);

        player.transform.position = newPosition;
    }

    public void FlyDown()
    {
        if (currentFlightIndex > 0)
        {
            currentFlightIndex--;
        }

        Vector3 newPosition = new Vector3(player.transform.position.x,
                                            flightLevels[currentFlightIndex],
                                            player.transform.position.z);

        player.transform.position = newPosition;
    }
}
