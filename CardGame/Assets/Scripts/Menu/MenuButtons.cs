using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public GameObject puzzleLevels;
    public GameObject options;

    public Options optionsScript;

    public LoadScene loadScene;

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
        if (!options.activeSelf && !puzzleLevels.activeSelf)
            SceneManager.LoadScene("Classic");
    }

    // Opens puzzle levels
    public void onPuzzlePerss()
    {
        if (!options.activeSelf)
            puzzleLevels.SetActive(true);
    }

    // Change scene to deck
    public void onDeckPress()
    {
        loadScene.LoadNewScene("Deck");
    }

    public void onOptionsPress()
    {
        puzzleLevels.SetActive(false);
        if (!options.activeSelf)
            optionsScript.displaySelectedValues();

        options.SetActive(!options.activeSelf);
    }

    // Turns off game
    public void onQuitPress()
    {
        Application.Quit();
    }
}
