using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCardsInDeck : MonoBehaviour
{

    public RectTransform content;           //parent for cards 
    public GameObject compactCardPrefab;    //card prefab
    public Sprite compactBackground;        //card background

    private List<string> cardsInDeck;       //list of all cards saved in deck
    private int count;                      //current count of cards in deck

    private List<Card> cards;               //list of card objects saved in deck
    private bool isCalledFromStart;         //if true updateCardDisplay is called from start method if false from update

    // Start is called before the first frame update
    void Start()
    {
        isCalledFromStart = true;
        cards = new List<Card>();
        cardsInDeck = SaveSystem.LoadDeck() == null ? null : SaveSystem.LoadDeck().cardsInDeck;     //gets cards from save file if file does not exists sets value to nu;;

        //if there are cards saved in deck displays them
        if (cardsInDeck != null)
            updateCardDisplay();

        isCalledFromStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if number of cards changed in deck and displays them
        cardsInDeck = SaveSystem.LoadDeck() == null ? null : SaveSystem.LoadDeck().cardsInDeck;
        if (cardsInDeck != null && count != cardsInDeck.Count)
            updateCardDisplay();
    }

    //displays cards in deck
    private void updateCardDisplay()
    {
        cards.Clear();

        //gets all cards in deck as card objects
        foreach (var c in cardsInDeck)
            cards.Add(Resources.Load<Card>("Cards/CreatedCards/" + c));

        //calculates height of content game object
        float rows = cards.Count;
        float height = rows == 1 ? 24 * 2 + 60 : (10 * rows + 24) + (60 * rows) + 14;

        //destroys all children of content object
        foreach (Transform child in content)
            Destroy(child.gameObject);

        content.sizeDelta = new Vector2(0, height); //sets height of content game object

        int row = 0;
        foreach (var c in cards)
        {
            float x = 0;    //x position of card

            if (!isCalledFromStart && cards.Count < 15)
                x = 190.0541f;
            else if (!isCalledFromStart && cards.Count >= 15)
                x = 181.5541f;

            float y = - 55 - row * (10 + 60);   //y position of card

            c.spawnCardCompact(compactCardPrefab, compactBackground, content.gameObject, 1, x, y);  //spawns card

            row++;
        }

        count = cards.Count;    //keeps track of card count in deck
    }


}
