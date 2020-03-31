using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeckSceneButtons : MonoBehaviour
{
    public DisplayAllCards displayAllCards;     // Reference to display all cards script.
    public InputField inputField;

    public LoadScene loadScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Returns back to menu.
    public void onBackPress()
    {
        loadScene.LoadNewScene("Menu");
    }

    //-----------------------------------------------------------
    // Buttons for filtering cards by cost.
    //-----------------------------------------------------------
    public void onCostAllPress()
    {
        displayAllCards.costFilter = 0;
        displayAllCards.displayAllCards();
    }

    public void onCost1Press()
    {
        displayAllCards.costFilter = 1;
        displayAllCards.displayAllCards();
    }

    public void onCost2Press()
    {
        displayAllCards.costFilter = 2;
        displayAllCards.displayAllCards();
    }

    public void onCost3Press()
    {
        displayAllCards.costFilter = 3;
        displayAllCards.displayAllCards();
    }

    public void onCost4Press()
    {
        displayAllCards.costFilter = 4;
        displayAllCards.displayAllCards();
    }

    public void onCost5Press()
    {
        displayAllCards.costFilter = 5;
        displayAllCards.displayAllCards();
    }

    public void onCost6Press()
    {
        displayAllCards.costFilter = 6;
        displayAllCards.displayAllCards();
    }

    public void onCost7Press()
    {
        displayAllCards.costFilter = 7;
        displayAllCards.displayAllCards();
    }

    public void onCost8Press()
    {
        displayAllCards.costFilter = 8;
        displayAllCards.displayAllCards();
    }

    public void onCost9Press()
    {
        displayAllCards.costFilter = 9;
        displayAllCards.displayAllCards();
    }

    public void onCost10Press()
    {
        displayAllCards.costFilter = 10;
        displayAllCards.displayAllCards();
    }

    //-----------------------------------------------------------

    // Performs search by inputted card name.
    public void onSearchInputChange()
    {
        displayAllCards.inputInSeachField = inputField.text.ToString();
        displayAllCards.displayAllCards();
    }
}
