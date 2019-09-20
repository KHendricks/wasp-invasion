using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences : MonoBehaviour
{
    private bool livesEnabled;
    private string characterSelected;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("characterSelected", "Frog");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetLivesEnabledStatus()
    {
        return livesEnabled;
    }

    public string GetCharacterSelected()
    {
        return characterSelected;
    }
}
