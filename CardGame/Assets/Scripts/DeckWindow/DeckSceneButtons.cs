using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckSceneButtons : MonoBehaviour
{
    public DisplayAllCards displayAllCards;     // Reference to display all cards script.
    public TMP_InputField inputField;

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

    // Performs search by card cost.
    public void onCostPress()
    {
        GameObject cost = EventSystem.current.currentSelectedGameObject;
        displayAllCards.costFilter = int.Parse(cost.name);
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
