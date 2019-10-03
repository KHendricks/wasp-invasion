using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    private GameObject newPlatform;
    private GameObject scriptManager;
    // Start is called before the first frame update
    void Start()
    {
        scriptManager = GameObject.Find("ScriptManager");
        int groundSelection;
        if (!PlayerPrefs.HasKey("GroundSelected"))
        {
            PlayerPrefs.SetInt("GroundSelect", 1);
        }

        do
        {
            groundSelection = Random.Range(1, 9);
            if (groundSelection == 9)
            {
                groundSelection = 1;
            }
        } while (groundSelection == PlayerPrefs.GetInt("GroundSelect"));
        PlayerPrefs.SetInt("GroundSelect", groundSelection);

        newPlatform = (GameObject)Resources.Load("Prefabs/Ground_" + groundSelection, typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {
    }

    void BeginSpawn()
    {
        if (Time.time - scriptManager.GetComponent<Pause>().startTime < 900)
        {
            GameObject platform =
                Instantiate(newPlatform,
                            new Vector3(gameObject.transform.parent.transform.position.x + 19,
                                        gameObject.transform.parent.transform.position.y,
                                        gameObject.transform.parent.transform.position.z),
                            Quaternion.identity);
        }
        // You made it to the end of the game
        else
        {
            Debug.Log("End of game. You played for 15 minutes, you win.");
            GameObject endPlatform = (GameObject)Resources.Load("Prefabs/EndGround", typeof(GameObject));
            GameObject platform =
                Instantiate(endPlatform,
                new Vector3(gameObject.transform.parent.transform.position.x + 19,
                            gameObject.transform.parent.transform.position.y,
                            gameObject.transform.parent.transform.position.z),
                Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            BeginSpawn();
        }
    }
}
