﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player;
    private GameObject waspEnemy;
    private GameObject hearts;
    private GameObject extraPoints;
    private GameObject poisonApple;
    private GameObject shootingStar;
    private GameObject pointText;
    private float startTime;
    private Vector2 spawnTimer;
    private ArrayList scoreBucket;
    private float[] flightLevels;
    private float waspSpawnOffset;
    private float starSpawnOffsetX, starSpawnOffsetY;
    private bool isWaspSpawning;
    private bool spawningThreeWasps;
    public bool stopSpawning;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        player = GameObject.FindWithTag("Player");
        pointText = GameObject.Find("PointText");

        waspEnemy = (GameObject)Resources.Load("Prefabs/Wasp", typeof(GameObject));
        shootingStar = (GameObject)Resources.Load("Prefabs/ShootingStar", typeof(GameObject));
        hearts = (GameObject)Resources.Load("Prefabs/ExtraLife", typeof(GameObject));
        extraPoints = (GameObject)Resources.Load("Prefabs/ExtraPoints", typeof(GameObject));
        poisonApple = (GameObject)Resources.Load("Prefabs/PoisonApple", typeof(GameObject));

        flightLevels = new float[3];
        flightLevels[0] = -.5f;
        flightLevels[1] = -.2f;
        flightLevels[2] = .2f;

        stopSpawning = false;
        isWaspSpawning = false;
        waspSpawnOffset = 3.5f;

        starSpawnOffsetX = 0;
        starSpawnOffsetY = 2;

        // Spawns a shooting star if the players score keeps decrementing
        // scoreBucket = new ArrayList();
        // InvokeRepeating("CheckScore", 5f, 2);

        // Set initial spawn timer for wasps
        SetSpawnTimer(5, 10);
        spawningThreeWasps = false;

        // Spawn Bonus Points
        InvokeRepeating("SpawnHearts", 5f, 17);
        InvokeRepeating("SpawnExtraPoints", 5f, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopSpawning)
        {
            if (!isWaspSpawning)
            {
                StartCoroutine(SpawnWasp());
            }
        }
    }

    void SpawnHearts()
    {
        int chance = Random.Range(0, 3);

        if (chance == 0)
        {
            if (player.GetComponent<PlayerControls>().lives < 3)
            {
                GameObject heart = Instantiate(hearts,
                                               new Vector3(player.transform.position.x + waspSpawnOffset,
                                                           flightLevels[Random.Range(0, 3)],
                                                           0),
                                               Quaternion.identity);
            }
        }
    }

    void SpawnExtraPoints()
    {
        int chance = Random.Range(0, 10);

        if (chance <= 5)
        {
            GameObject points = Instantiate(extraPoints,
                                           new Vector3(player.transform.position.x + waspSpawnOffset,
                                                       flightLevels[Random.Range(0, 3)],
                                                       0),
                                           Quaternion.identity);
        }
        else if (chance == 9)
        {
            GameObject points = Instantiate(poisonApple,
                               new Vector3(player.transform.position.x + waspSpawnOffset,
                                           flightLevels[Random.Range(0, 3)],
                                           0),
                               Quaternion.identity);
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
        float timeToSpawn = Time.time;
        yield return new WaitForSeconds(Random.Range(spawnTimer[0], spawnTimer[1]));
        SpawnWaspEntity();
        isWaspSpawning = false;
    }

    void SpawnWaspEntity()
    {
        // Spawn Wasp(s) based on difficutly.
        // Spawn a single wasp based on direction of player
        if (Time.time - startTime < 35)
        {
            SetSpawnTimer(5, 10);
            SpawnSingleWasp();
        }

        // Speed up spawn rate of a single wasp
        else if (Time.time - startTime < 60)
        {
            SetSpawnTimer(1, 6);
            SpawnSingleWasp();
        }

        // Spawn two wasps slowly
        else if (Time.time - startTime < 85)
        {
            SetSpawnTimer(5, 10);

            int firstSpawnIndex = Random.Range(0, 3);
            int secondSpawnIndex;
            do
            {
                secondSpawnIndex = Random.Range(0, 3);
            } while (firstSpawnIndex == secondSpawnIndex);

            SpawnSingleWasp(firstSpawnIndex);
            SpawnSingleWasp(secondSpawnIndex);
        }

        // Spawn two wasps quickly
        else if (Time.time - startTime < 110)
        {
            SetSpawnTimer(1, 5);

            int firstSpawnIndex = Random.Range(0, 3);
            int secondSpawnIndex;
            do
            {
                secondSpawnIndex = Random.Range(0, 3);
            } while (firstSpawnIndex == secondSpawnIndex);

            SpawnSingleWasp(firstSpawnIndex);
            SpawnSingleWasp(secondSpawnIndex);
        }

        // Time to make the game hard
        else if (Time.time - startTime < 135)
        {
            SetSpawnTimer(1, 5);

            if (!spawningThreeWasps)
            {
                StartCoroutine(SpawnThreeWasps(.9f));
            }
        }

        // Time to make the game really hard
        else if (Time.time - startTime < 180 )
        {
            SetSpawnTimer(2, 2);

            if (!spawningThreeWasps)
            {
                StartCoroutine(SpawnThreeWasps(.7f));
            }
        }

        // They're still alive?
        else
        {
            SetSpawnTimer(2, 2);

            int spawnChoice = Random.Range(0, 2);
            if (spawnChoice == 0)
            {
                if (!spawningThreeWasps)
                {
                    StartCoroutine(SpawnThreeWasps(.5f));
                }
            }
            else if (spawnChoice == 1)
            {
                int firstSpawnIndex = Random.Range(0, 3);
                int secondSpawnIndex;
                do
                {
                    secondSpawnIndex = Random.Range(0, 3);
                } while (firstSpawnIndex == secondSpawnIndex);

                SpawnSingleWasp(firstSpawnIndex);
                SpawnSingleWasp(secondSpawnIndex);
            }
        }
    }

    GameObject SpawnSingleWasp()
    {
        if (player.GetComponent<SpriteRenderer>().flipX)
        {
            waspSpawnOffset *= -1;
        }
        else
        {
            waspSpawnOffset = Mathf.Abs(waspSpawnOffset);
        }

        GameObject wasp = Instantiate(waspEnemy,
                                      new Vector3(player.transform.position.x + waspSpawnOffset,
                                                  flightLevels[Random.Range(0, 3)],
                                                  0),
                                      Quaternion.identity);

        if (waspSpawnOffset < 0)
        {
            wasp.GetComponent<SpriteRenderer>().flipX = false;
        }

        return wasp;
    }

    GameObject SpawnSingleWasp(int spawnIndex)
    {
        if (player.GetComponent<SpriteRenderer>().flipX)
        {
            waspSpawnOffset *= -1;
        }
        else
        {
            waspSpawnOffset = Mathf.Abs(waspSpawnOffset);
        }

        GameObject wasp = Instantiate(waspEnemy,
                                      new Vector3(player.transform.position.x + waspSpawnOffset,
                                                  flightLevels[spawnIndex],
                                                  0),
                                      Quaternion.identity);

        if (waspSpawnOffset < 0)
        {
            wasp.GetComponent<SpriteRenderer>().flipX = false;
        }

        return wasp;
    }

    IEnumerator SpawnThreeWasps(float delay)
    {
        spawningThreeWasps = true;
        int[] spawnIndexes = new int[3];
        spawnIndexes[0] = Random.Range(0, 3);

        do
        {
            spawnIndexes[1] = Random.Range(0, 3);
        } while (spawnIndexes[0] == spawnIndexes[1]);

        do
        {
            spawnIndexes[2] = Random.Range(0, 3);
        } while (spawnIndexes[2] == spawnIndexes[0] ||
                 spawnIndexes[2] == spawnIndexes[1]);

        SpawnSingleWasp(spawnIndexes[0]);
        yield return new WaitForSeconds(delay);
        SpawnSingleWasp(spawnIndexes[1]);
        yield return new WaitForSeconds(delay);
        SpawnSingleWasp(spawnIndexes[2]);

        spawningThreeWasps = false;
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
