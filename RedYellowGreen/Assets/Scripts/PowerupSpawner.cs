using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    private GameObject shieldPrefab;
    private GameObject player;
    private float shieldSpawnOffsetX, shieldSpawnOffsetY;

    // Start is called before the first frame update
    void Start()
    {
        shieldSpawnOffsetX = 1.5f;
        shieldSpawnOffsetY = 1.5f;

        player = GameObject.FindWithTag("Player");
        shieldPrefab = (GameObject)Resources.Load("Prefabs/Shield", typeof(GameObject));

        InvokeRepeating("SpawnShield", 5, 3);
    }

    void SpawnShield()
    {

        int spawnChance = Random.Range(0, 5);

        if (spawnChance == 0)
        {
            GameObject shield =
                        Instantiate(shieldPrefab,
                        new Vector3(player.transform.position.x + shieldSpawnOffsetX,
                                    player.transform.position.y + shieldSpawnOffsetY,
                                    player.transform.position.z),
                        Quaternion.identity);
        }
        else
        {
            Debug.Log("Did not spawn");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
