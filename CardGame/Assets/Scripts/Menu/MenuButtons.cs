using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public GameObject puzzleLevels;
    public GameObject options;

    public Options optionsScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Opens classic scene
    public void onClassicPress()
    {
        SceneManager.LoadScene("Classic");
    }

    // Opens puzzle levels
    public void onPuzzlePerss()
    {
        puzzleLevels.SetActive(true);
    }

    // Change scene to deck
    public void onDeckPress()
    {
        SceneManager.LoadScene("Deck");
    }

    public void onOptionsPress()
    {
        optionsScript.displaySelectedValues();
        options.SetActive(true);
    }

    // Turns off game
    public void onQuitPress()
    {
        Application.Quit();
    }
}
