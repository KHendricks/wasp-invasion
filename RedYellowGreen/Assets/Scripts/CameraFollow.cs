using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    public Vector3 offset;
    public float smoothSpeed;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void LateUpdate()
    {

        Vector3 desiredPosition = new Vector3(player.transform.position.x + offset.x, 
                                                gameObject.transform.position.y, 
                                                player.transform.position.z + offset.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position,
                                                desiredPosition, 
                                                smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
