using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DisplayAllCards : MonoBehaviour
{
    private int ROW_START = -516;       //x coordinates of first card in row
    private int ROW_OFFSET = 344;       //offset for placing next card in row
    private int LINE_OFFSET = 478;      //offset for placing cards in next line

    public GameObject cardPrefab;       //card prefab
    public Sprite minionBackground;     //minion card background
    public Sprite spellBackground;      //spell card background
    public RectTransform content;       //parent for cards 

    private List<Card> cards;           //array that contains all cards

    // Start is called before the first frame update
    void Start()
    {
        //gets all cards
        cards = Resources.LoadAll<Card>("Cards/CreatedCards").ToList();

        ////calculates height of content game object
        float rows = (float)Math.Ceiling(cards.Count / 4f);
        float height = rows == 1 ? 24 * 2 + 455 : (24 * rows + 24) + (455 * rows);
        content.sizeDelta = new Vector2(0, height);

        int cardNumber = 1;     //card number in deck
        foreach (var c in cards)
        {
            float row = (float)Math.Ceiling(cardNumber / 4f);           //row in which card is
            float cardNumberInRow = cardNumber - ((row - 1) * 4) - 1;   //card number in row from 0 to 3
            float x = ROW_START + ROW_OFFSET * cardNumberInRow;         //x position of card
            float y = -251.5f - (row - 1) * LINE_OFFSET;                //y position of card

            //picks background according to card type
            if (c.type.ToLower() == "spell")
                c.spawnCard(cardPrefab, spellBackground, content.gameObject, x, y);     //spawns spell
            else
                c.spawnCard(cardPrefab, minionBackground, content.gameObject, x, y);    //spawns minion

            cardNumber++;
        }
    }
}
