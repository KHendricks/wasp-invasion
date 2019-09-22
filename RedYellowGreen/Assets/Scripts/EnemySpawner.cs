using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player;
    private GameObject waspEnemy;

    private float[] flightLevels;
    private float spawnOffset;
    private bool isWaspSpawning;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        waspEnemy = (GameObject)Resources.Load("Prefabs/Wasp", typeof(GameObject));

        flightLevels = new float[3];
        flightLevels[0] = -.5f;
        flightLevels[1] = -.2f;
        flightLevels[2] = .2f;
        spawnOffset = 7f;

        isWaspSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaspSpawning)
        {
            StartCoroutine(SpawnWasp());
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
                    new Vector3(player.transform.position.x + spawnOffset, flightLevels[spawnIndex], 0), 
                    Quaternion.identity);
    }
}
