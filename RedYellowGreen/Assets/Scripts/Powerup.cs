using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private Vector3 spawnLocation;
    private GameObject player;
    private float spawnOffsetX, spawnOffsetY;
    private bool isFlashing;

    // Start is called before the first frame update
    public void Start()
    {
        player = GameObject.FindWithTag("Player");

        isFlashing = false;
        SetSpawnLocation(new Vector3(player.transform.position.x + spawnOffsetX,
                                     player.transform.position.y + spawnOffsetY,
                                     player.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpawnLocation(Vector3 spawnLocation)
    {
        this.spawnLocation = spawnLocation;
    }

    public Vector3 GetSpawnLocation()
    {
        return spawnLocation;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            if (!isFlashing)
            {
                StartCoroutine(StartFlashing());
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator StartFlashing()
    {
        isFlashing = true;

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        yield return new WaitForSeconds(2f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, .5f);
        yield return new WaitForSeconds(.5f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        yield return new WaitForSeconds(.5f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, .5f);
        yield return new WaitForSeconds(.5f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        yield return new WaitForSeconds(.5f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, .5f);
        yield return new WaitForSeconds(.5f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        yield return new WaitForSeconds(.5f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, .5f);
        yield return new WaitForSeconds(.2f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, .25f);
        yield return new WaitForSeconds(.2f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, .5f);
        yield return new WaitForSeconds(.2f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, .25f);
        yield return new WaitForSeconds(.2f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, .5f);
        yield return new WaitForSeconds(.2f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, .25f);
        yield return new WaitForSeconds(.2f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, .5f);
        yield return new WaitForSeconds(.2f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, .25f);
        yield return new WaitForSeconds(.5f);

        Destroy(gameObject);

        isFlashing = false;
    }
}
