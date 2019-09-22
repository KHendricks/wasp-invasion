using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health;
    private int speed;


    // Start is called before the first frame update
    void Start()
    {

        }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetHealth()
    {
        return this.health;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public int GetSpeed()
    {
        return this.speed;
    }
}
