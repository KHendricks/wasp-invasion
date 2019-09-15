using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    private bool livesEnabled;

    // Start is called before the first frame update
    void Start()
    {
        livesEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetLivesEnabledStatus()
    {
        return livesEnabled;
    }
}
