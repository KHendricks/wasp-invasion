using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float yRotation;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, yRotation, 0);
    }
}
