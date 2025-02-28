﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectCards : MonoBehaviour
{
    public RectTransform content;                   // Parent for cards.
    public TextMeshProUGUI amountOfCardsInDeck;     // Amount of cards in deck.
    public int maxCountOfCards;                     // Maximum amount of cards in deck.
    public int maxCountOfCopies;                    // Maximum amount of same card in deck.
    public DisplayCardsInDeck displayCardsInDeck;   // Reference to display all cards in deck script.
    public Transform hoverDisplay;                  // Parent for displaying cards that are hovered over.

    private GraphicRaycaster raycaster;             // Raycast to check what objects are under mouse cursor.
    private bool locker = false;                    // If true user has pressed left mouse button on card but haven't released yet.


    private List<RaycastResult> results;            // Objects hit by raycast when user pressed left mouse button.
    private List<RaycastResult> resultsRelease;     // Objects hit by raycast when user released left mouse button.
    private float startPos;                         // Position of content game object when user pressed left mouse button.
    private float endPos;                           // Position of content game object when user released left mouse button.

    private List<DeckFormat> cardsInDeck;           // List of cards in deck.

    private int cardsInDeckCount;                   // Amount of cards in deck.


    // Start is called before the first frame update
    public void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();

        cardsInDeck = SaveSystem.LoadDeck() == null ? new List<DeckFormat>() : SaveSystem.LoadDeck().cardsInDeck;   //gets cards from save file if file does not exists sets value to null

        // If there are cards saved in deck displays them.
        if (cardsInDeck != null)
            displayCardsInDeck.updateCardDisplay(cardsInDeck);

        // Counts amount of cards in deck.
        cardsInDeckCount = 0;
        foreach (var c in cardsInDeck)
            cardsInDeckCount += c.count;
    }

    // Update is called once per frame
    public void Update()
    {
        amountOfCardsInDeck.text = cardsInDeckCount + "/" + maxCountOfCards + "cards"; //displays amount of cards in deck

        //changes color of text for card amount in deck
        if (cardsInDeckCount >= maxCountOfCards)
            amountOfCardsInDeck.color = Color.red;
        else
            amountOfCardsInDeck.color = Color.black;



        GameObject card = selectCard();
        if (card != null )
        {
            //removes selected cards from deck
            if (card.gameObject.name.Contains("CardInDeck - "))
                removeFromDeck(card);

            //adds selected cards to deck
            else if (card.gameObject.name.Contains("Background - ") && cardsInDeckCount < maxCountOfCards)
                addToDeck(card);

            // Refreshes deck view.
            displayCardsInDeck.updateCardDisplay(cardsInDeck);

            cardsInDeckCount = 0;
            foreach (var ca in cardsInDeck)
                cardsInDeckCount += ca.count;
        }
    }

    //adds card to deck
    public void addToDeck(GameObject c)
    {
        string cardName = c.name.Replace("Background - ", "");
        foreach (var card in cardsInDeck)
        {
            //if card exists and there are less than allowed amount of them in deck increases counter
            if (card.name == cardName && card.count < maxCountOfCopies)
            {
                card.count++;
                SaveSystem.SaveDeck(cardsInDeck);
                return;
            }

            //if there are maximum amount of selected card in deck addition of card is discarded
            else if (card.name == cardName && card.count >= maxCountOfCopies)
                return;
        }

        //if card does not exists in deck already adds it
        cardsInDeck.Add( new DeckFormat(c.name.Replace("Background - ", ""), 1));
        SaveSystem.SaveDeck(cardsInDeck);
    }

    //removes cards form deck
    public void removeFromDeck(GameObject c)
    {
        string cardName = c.name.Replace("CardInDeck - ", "");
        foreach (var card in cardsInDeck)
        {
            //if card exists and there more than one than lover count by one
            if (card.name == cardName && card.count > 1)
            {
                card.count--;
                SaveSystem.SaveDeck(cardsInDeck);
                return;
            }

            //if there are only one copy of card removed it
            else if (card.name == cardName && card.count == 1)
            {
                cardsInDeck.Remove(card);
                SaveSystem.SaveDeck(cardsInDeck);

                //destroys all children of content object
                foreach (Transform child in hoverDisplay)
                    Destroy(child.gameObject);

                return;
            }
        }
    }

    //gets card that is under mouse cursor
    private GameObject selectCard()
    {
        if (!locker)
        {
            results = new List<RaycastResult>();
            resultsRelease = new List<RaycastResult>();
            startPos = 0;
            endPos = 0;
        }

        //casts ray when mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {

            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;
            raycaster.Raycast(pointerData, results);

            startPos = content.transform.position.y;

            locker = true;
        }

        //casts ray when mouse button is released and compares results if they match returns selected card
        if (Input.GetMouseButtonUp(0))
        {
            PointerEventData pointerDataRelease = new PointerEventData(EventSystem.current);
            pointerDataRelease.position = Input.mousePosition;

            raycaster.Raycast(pointerDataRelease, resultsRelease);

            endPos = content.transform.position.y;

            GameObject card = null;
            GameObject cardRelease = null;
            string inSelectionOrDeck = null;


            foreach (RaycastResult result in results)
            {
                if (result.gameObject.name.Contains("Background - ") || result.gameObject.name.Contains("CardInDeck - "))
                {
                    card = result.gameObject;
                    inSelectionOrDeck = card.name.Split(' ')[0];
                }
                   
            }

            foreach (RaycastResult result in resultsRelease)
            { 
                if (result.gameObject.name.Contains(inSelectionOrDeck + " - "))
                    cardRelease = result.gameObject;
            }

            locker = false;


            if (card == cardRelease && startPos == endPos && cardRelease != null)
                return cardRelease;
        }

        return null;
    }
}
