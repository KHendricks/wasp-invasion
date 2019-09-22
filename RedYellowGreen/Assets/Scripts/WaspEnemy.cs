using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        SetHealth(3);
        SetSpeed(-1.5f);
        Debug.Log(GetHealth());
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + dirX,
                                            transform.position.y,
                                            transform.position.z);
    }
}
