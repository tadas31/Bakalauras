using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DisplayAllCards : MonoBehaviour
{
    public RectTransform content;       //field where all cards will be placed
    public GameObject cardPrefab;       //card prefab
    public Sprite minionBackground;     //minion card background
    public Sprite spellBackground;      //spell card background

    private AllCards cards;
    private Hashtable allCards;
    private ICollection key;

    private int ROW_START = -516;       //x coordinates of first card in row
    private int ROW_OFFSET = 344;       //offset for placing next card in row
    private int LINE_OFFSET = 478;      //offset for placing cards in next line

    // Start is called before the first frame update
    void Start()
    {
        //gets all cards 
        cards = new AllCards();
        allCards = cards.getCards();
        key = allCards.Keys;

        //calculates height of content game object
        float rows = (float)Math.Ceiling(allCards.Count / 4f);
        float height = rows == 1 ? 24 * 2 + 455 : (24 * rows + 24) + (455 * rows);
        content.sizeDelta = new Vector2( 0, height);


        int cardNumber = 1;     //card number in deck
        //loops through all cards and spawns them in game
        foreach (var val in key)
        {
            float row = (float)Math.Ceiling(cardNumber / 4f);           //row in which card is
            float cardNumberInRow = cardNumber - ((row - 1) * 4) - 1;   //card number in row from 0 to 3
            float x = ROW_START + ROW_OFFSET * cardNumberInRow;         //x position of card
            float y = -251.5f - (row - 1) * LINE_OFFSET;                //y position of card

            Card c = (Card)allCards[val];   //card to spawn
            spawnCard(c, x, y);             //spawns card

            cardNumber++;
        }

    }

    // Update is called once per frame
    void Update()
    {
           
    }

    //spawns card in game and sets all values for necessary fields
    public void spawnCard(Card c, float x, float y)
    {
        GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
        newCard.GetComponent<Transform>().SetParent(content.transform);
        newCard.transform.localPosition = new Vector3(x, y, 0);
        Debug.Log(newCard.transform.localRotation);


        newCard.transform.localScale = Vector3.one;

        foreach (var child in newCard.GetComponentsInChildren<Transform>())
        {
            switch (child.name)
            {
                case "Background":
                    if (c.type.ToLower() != "spell")
                        child.GetComponent<Image>().sprite = minionBackground;
                    else
                        child.GetComponent<Image>().sprite = spellBackground;
                    break;
                case "Title":
                    child.GetComponent<TextMeshProUGUI>().text = c.title;
                    break;
                case "Cost":
                    child.GetComponent<TextMeshProUGUI>().text = c.cost.ToString();
                    break;
                case "Image":
                    Sprite image = Resources.Load<Sprite>(c.image);
                    child.GetComponent<Image>().sprite = image;
                    break;
                case "Type":
                    child.GetComponent<TextMeshProUGUI>().text = c.type;
                    break;
                case "Description":
                    child.GetComponent<TextMeshProUGUI>().text = c.description;
                    break;
                case "Stats":
                    if (c.type.ToLower() != "spell")
                        child.GetComponent<TextMeshProUGUI>().text = c.attack + " / " + c.life;
                    else
                        child.GetComponent<TextMeshProUGUI>().text = null;
                    break;
            }
        }
    }
}
