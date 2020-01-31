using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckSceneButtons : MonoBehaviour
{
    public DisplayAllCards displayAllCards;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBackPress()
    {
        SceneManager.LoadScene("Menu");
    }

    //-----------------------------------------------------------
    //Buttons for filtering cards by cost
    //-----------------------------------------------------------
    public void onCostAllPress()
    {
        displayAllCards.displayAllCards(0);
    }

    public void onCost1Press()
    {
        displayAllCards.displayAllCards(1);
    }

    public void onCost2Press()
    {
        displayAllCards.displayAllCards(2);
    }

    public void onCost3Press()
    {
        displayAllCards.displayAllCards(3);
    }

    public void onCost4Press()
    {
        displayAllCards.displayAllCards(4);
    }

    public void onCost5Press()
    {
        displayAllCards.displayAllCards(5);
    }

    public void onCost6Press()
    {
        displayAllCards.displayAllCards(6);
    }

    public void onCost7Press()
    {
        displayAllCards.displayAllCards(7);
    }

    public void onCost8Press()
    {
        displayAllCards.displayAllCards(8);
    }

    public void onCost9Press()
    {
        displayAllCards.displayAllCards(9);
    }

    public void onCost10Press()
    {
        displayAllCards.displayAllCards(10);
    }

    //-----------------------------------------------------------


}
