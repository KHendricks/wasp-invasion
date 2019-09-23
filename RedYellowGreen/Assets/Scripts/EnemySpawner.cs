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

    private ArrayList scoreBucket;
    private float[] flightLevels;
    private float waspSpawnOffset;
    private float starSpawnOffsetX, starSpawnOffsetY;
    private bool isWaspSpawning;
    private bool isStarSpawning;

    // Start is called before the first frame update
    void Start()
    {
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
        starSpawnOffsetX = 10;
        starSpawnOffsetY = 10;

        // Spawns a shooting star if the players score keeps decrementing
        scoreBucket = new ArrayList();
        InvokeRepeating("CheckScore", 5f, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaspSpawning)
        {
            StartCoroutine(SpawnWasp());
        }

        if (!isStarSpawning)
        {

        }

    }

    void CheckScore()
    {
        scoreBucket.Add(System.Convert.ToInt32(pointText.GetComponent<Text>().text));
        if (scoreBucket.Count > 1)
        {
            Debug.Log("YO");
            if ((int)scoreBucket[scoreBucket.Count - 2] > (int)scoreBucket[scoreBucket.Count - 1])
            {
                SpawnShootingStar();
            }
        }
    }

    IEnumerator SpawnWasp()
    {
        isWaspSpawning = true;
        yield return new WaitForSeconds(Random.Range(5, 10));
        SpawnWaspEntity();
        isWaspSpawning = false;
    }

    void SpawnWaspEntity()
    {
        int spawnIndex;
        spawnIndex = Random.Range(0, 3);
        Instantiate(waspEnemy, 
                    new Vector3(player.transform.position.x + waspSpawnOffset, flightLevels[spawnIndex], 0), 
                    Quaternion.identity);
    }

    void SpawnShootingStar()
    {
        scoreBucket.Clear();
        Instantiate(shootingStar,
                    new Vector3(player.transform.position.x + starSpawnOffsetX,
                                player.transform.position.y + starSpawnOffsetY, 
                                0),
                    Quaternion.identity);
    }
}
