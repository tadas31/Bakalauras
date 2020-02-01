﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DisplayAllCards : MonoBehaviour
{
    private int ROW_START = -516;       // X coordinates of first card in row.
    private int ROW_OFFSET = 344;       // Offset for placing next card in row.
    private int LINE_OFFSET = 478;      // Offset for placing cards in next line.

    public GameObject cardPrefab;       // Card prefab.
    public Sprite minionBackground;     // Minion card background.
    public Sprite spellBackground;      // Spell card background.
    public RectTransform content;       // Parent for cards.

    public int costFilter;              // Card cost to filter by.
    public string inputInSeachField;    // String from search input field.
    public string tab;                  // Which tab to display.

    // Start is called before the first frame update
    void Start()
    {
        costFilter = 0;
        tab = "All";
        displayAllCards();
    }

    public void displayAllCards()
    {
        //destroys all children of content object
        foreach (Transform child in content)
            Destroy(child.gameObject);

        //gets all cards
        List<Card> cards = Resources.LoadAll<Card>("Cards/CreatedCards").ToList();

        // Filter by cost.
        if (costFilter != 0)
            cards = filterByCost(cards);

        // Search by name
        if (inputInSeachField != "" || inputInSeachField != null)
            cards = searchByName(cards);

        // Filters cards by current tab
        if (tab != "All")
            cards = filterByTab(cards);

        ////calculates height of content game object
        float rows = (float)Math.Ceiling(cards.Count / 4f);
        float height = rows == 1 ? 24 * 2 + 455 : (24 * rows + 24) + (455 * rows);
        content.sizeDelta = new Vector2(0, height);

        int cardNumber = 1;     //card number in deck
        foreach (var c in cards)
        {
            float row = (float)Math.Ceiling(cardNumber / 4f);           //row in which card is
            float cardNumberInRow = cardNumber - ((row - 1) * 4) - 1;   //card number in row from 0 to 3
            float x =  ROW_START + ROW_OFFSET * cardNumberInRow;        //x position of card
            float y = -251.5f - (row - 1) * LINE_OFFSET;                //y position of card

            //picks background according to card type
            if (c.type.ToLower() == "spell")
                c.spawnCard(cardPrefab, spellBackground, content.gameObject, x, y);     //spawns spell
            else
                c.spawnCard(cardPrefab, minionBackground, content.gameObject, x, y);    //spawns minion

            cardNumber++;
        }
    }

    // Filters out cards by cost and returns list of those cards.
    public List<Card> filterByCost(List<Card> cards)
    {
        List<Card> filteredList = new List<Card>();
        foreach (var c in cards)
        {
            if (costFilter == c.cost)
                filteredList.Add(c);
        }

        return filteredList;
    }

    // Searches cards by name.
    public List<Card> searchByName(List<Card> cards)
    {
        List<Card> filteredList = new List<Card>();
        foreach (var c in cards)
        {
            if (c.name.ToLower().Contains(inputInSeachField.ToLower()))
                filteredList.Add(c);
        }

        return filteredList;
    }

    // Filters cards by current tab
    public List<Card> filterByTab(List<Card> cards)
    {
        List<Card> filteredList = new List<Card>();
        foreach (var c in cards)
        {
            if (tab == "Minions" && c.type.ToLower() != "spell")
                filteredList.Add(c);

            else if (tab == "Spells" && c.type.ToLower() == "spell")
                filteredList.Add(c);
        }

        return filteredList;
    }
}
