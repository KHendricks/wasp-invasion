using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public Rigidbody playButtonRb;

    private bool currentlyWaiting = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentlyWaiting);
    }

    // Button is defaulted to:
    // is kinematic = true
    // use gravity = false
    public void PlayButtonPress()
    {
        // Values are arbitrary. Sometimes the button can move really slowly or
        // very quickly. 
        Vector3 playButtonVel = new Vector3(Random.Range(-1000, 1000), Random.Range(-1200, 1200), 0);
        playButtonRb.isKinematic = false;
        playButtonRb.velocity = playButtonVel;

        // This is so the button can't be pressed again and change the direction
        gameObject.GetComponent<Button>().interactable = false;
        TimeDelay(3);

    }

    void TimeDelay(int x)
    {
        if (!currentlyWaiting)
        {
            StartCoroutine(TimeDelayHelper(x));
        }
    }

    IEnumerator TimeDelayHelper(int x)
    {
        currentlyWaiting = true;
        yield return new WaitForSeconds(x);
        currentlyWaiting = false;
    }
}
