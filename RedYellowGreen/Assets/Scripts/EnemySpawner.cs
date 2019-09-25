using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player;
    private GameObject waspEnemy;
    private GameObject shootingStar;
    private GameObject pointText;
    private float startTime;
    private Vector2 spawnTimer;
    private ArrayList scoreBucket;
    private float[] flightLevels;
    private float waspSpawnOffset;
    private float starSpawnOffsetX, starSpawnOffsetY;
    private bool isWaspSpawning;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        player = GameObject.FindWithTag("Player");
        pointText = GameObject.Find("PointText");

        waspEnemy = (GameObject)Resources.Load("Prefabs/Wasp", typeof(GameObject));
        shootingStar = (GameObject)Resources.Load("Prefabs/ShootingStar", typeof(GameObject));

        flightLevels = new float[3];
        flightLevels[0] = -.5f;
        flightLevels[1] = -.2f;
        flightLevels[2] = .2f;

        isWaspSpawning = false;
        waspSpawnOffset = 7f;

        isStarSpawning = false;
        starSpawnOffsetX = 0;
        starSpawnOffsetY = 2;

        // Spawns a shooting star if the players score keeps decrementing
        scoreBucket = new ArrayList();
        waspSpawnOverride = false;
        InvokeRepeating("CheckScore", 5f, 2);

        // Set initial spawn timer for wasps
        SetSpawnTimer(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaspSpawning)
        {
            StartCoroutine(SpawnWasp());
        }
    }

    void CheckScore()
    {
        scoreBucket.Add(System.Convert.ToInt32(pointText.GetComponent<Text>().text));
        if (scoreBucket.Count > 1)
        {
            if ((int)scoreBucket[scoreBucket.Count - 2] > (int)scoreBucket[scoreBucket.Count - 1])
            {
                SpawnShootingStar();
            }
        }
    }

    void SetSpawnTimer(float x, float y)
    {
        this.spawnTimer = new Vector2(x, y);
    }

    IEnumerator SpawnWasp()
    {
        isWaspSpawning = true;
        Debug.Log("BEFORE: " + Time.time);
        yield return new WaitForSeconds(Random.Range(spawnTimer[0], spawnTimer[1]));
        Debug.Log("AFTER: " + Time.time);
        SpawnWaspEntity();
        isWaspSpawning = false;
    }

    void SpawnWaspEntity()
    {
        int spawnIndex;
        spawnIndex = Random.Range(0, 3);
        if (player.GetComponent<SpriteRenderer>().flipX)
        {
            waspSpawnOffset *= -1;
        }

        // Spawn Wasp(s) based on difficutly.
        // Spawn a single wasp based on direction of player
        if (Time.time - startTime < 45)
        {
            GameObject wasp =
                        Instantiate(waspEnemy,
                        new Vector3(player.transform.position.x + waspSpawnOffset, flightLevels[spawnIndex], 0),
                        Quaternion.identity);

            if (waspSpawnOffset < 0)
            {
                wasp.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        // Speed up spawn rate of a single wasp
        else if (Time.time - startTime < 90)
        {
            SetSpawnTimer(1, 6);

            GameObject wasp =
            Instantiate(waspEnemy,
            new Vector3(player.transform.position.x + waspSpawnOffset, flightLevels[spawnIndex], 0),
            Quaternion.identity);

            if (waspSpawnOffset < 0)
            {
                wasp.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    // Star only spawns when player is stopped and should be moving
    // This mechanic is intended
    void SpawnShootingStar()
    {
        scoreBucket.Clear();

        if (player.GetComponent<PlayerControls>().GetMovementState() == "Stopped")
        {
            Instantiate(shootingStar,
                        new Vector3(player.transform.position.x + starSpawnOffsetX,
                                    player.transform.position.y + starSpawnOffsetY,
                                    0),
                        Quaternion.identity);
        }
    }
}
