using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System;

public class TabButton : MonoBehaviour, IPointerClickHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{

    public TextMeshProUGUI tabName;         // Name of tab.
    public DisplayAllCards displayAllCards; // Reference to display all cards script
    public TabControler tabController;      // Reference to tab controller script
    public bool isClicked;                  // If tab is clicked value is true else it's false

    // Start is called before the first frame update
    void Start()
    {
        isClicked = false;
    }

    // On click changes displayed cards according to tab name
    public void OnPointerClick(PointerEventData eventData)
    {
        displayAllCards.tab = tabName.text;
        displayAllCards.displayAllCards();
        tabController.undoClick();
        GetComponent<Image>().color = Color.yellow;
        isClicked = true;
    }

    // Changes look of tab when hovering over
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked)
            GetComponent<Image>().color = Color.yellow;
    }

    // Undoes changes made by OnPointerEnter
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked)
            GetComponent<Image>().color = Color.gray;
    }
}
