using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Opens classic scene
    public void onClassicPress()
    {
        SceneManager.LoadScene("Classic");
    }

    //change scene to deck
    public void onDeckPress()
    {
        SceneManager.LoadScene("Deck");
    }

    public void onOptionsPress()
    {
        //change scene or open window (undecided)
    }

    //turns off game
    public void onQuitPress()
    {
        Application.Quit();
    }
}
