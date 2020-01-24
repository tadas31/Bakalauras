using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public GameObject cardPrefab;
    public Sprite minionBackground;
    public Sprite spellBackground;
    public RectTransform content;

    private int ROW_START = -516;       //x coordinates of first card in row
    private int ROW_OFFSET = 344;       //offset for placing next card in row
    private int LINE_OFFSET = 478;      //offset for placing cards in next line


    private Minion minion;
    private Spell spell;

    // Start is called before the first frame update
    void Start()
    {
        minion = Resources.Load<Minion>("Cards/MinionCards/Goblin");
        spell = Resources.Load<Spell>("Cards/SpellCards/Spell");


        List<Minion> minionCards = new List<Minion>();
        List<Spell>spellCards = new List<Spell>();
        for (int i = 0; i < 29; i++)
        {
            minionCards.Add(minion);
            spellCards.Add(spell);
        }
            

        ////calculates height of content game object
        float rows = (float)Math.Ceiling((minionCards.Count + spellCards.Count) / 4f);
        float height = rows == 1 ? 24 * 2 + 455 : (24 * rows + 24) + (455 * rows);
        content.sizeDelta = new Vector2(0, height);

        int cardNumber = 1;     //card number in deck
        foreach (var c in minionCards)
        {
            float row = (float)Math.Ceiling(cardNumber / 4f);           //row in which card is
            float cardNumberInRow = cardNumber - ((row - 1) * 4) - 1;   //card number in row from 0 to 3
            float x = ROW_START + ROW_OFFSET * cardNumberInRow;         //x position of card
            float y = -251.5f - (row - 1) * LINE_OFFSET;                //y position of card

            GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
            newCard.GetComponent<Transform>().SetParent(content.transform);
            newCard.transform.localPosition = new Vector3(x, y, 0);

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
                        child.GetComponent<TextMeshProUGUI>().text = c.cardName;
                        break;
                    case "Cost":
                        child.GetComponent<TextMeshProUGUI>().text = c.cost.ToString();
                        break;
                    case "Image":
                        child.GetComponent<Image>().sprite = c.image;
                        break;
                    case "Type":
                        child.GetComponent<TextMeshProUGUI>().text = c.type;
                        break;
                    case "Description":
                        child.GetComponent<TextMeshProUGUI>().text = c.description;
                        break;
                    case "Stats":
                        if (c.type.ToLower() != "spell")
                            child.GetComponent<TextMeshProUGUI>().text = c.attack + " / " + minion.life;
                        else
                            child.GetComponent<TextMeshProUGUI>().text = null;
                        break;
                }
            }

            cardNumber++;
        }

        foreach (var c in spellCards)
        {
            float row = (float)Math.Ceiling(cardNumber / 4f);           //row in which card is
            float cardNumberInRow = cardNumber - ((row - 1) * 4) - 1;   //card number in row from 0 to 3
            float x = ROW_START + ROW_OFFSET * cardNumberInRow;         //x position of card
            float y = -251.5f - (row - 1) * LINE_OFFSET;                //y position of card

            GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
            newCard.GetComponent<Transform>().SetParent(content.transform);
            newCard.transform.localPosition = new Vector3(x, y, 0);

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
                        child.GetComponent<TextMeshProUGUI>().text = c.cardName;
                        break;
                    case "Cost":
                        child.GetComponent<TextMeshProUGUI>().text = c.cost.ToString();
                        break;
                    case "Image":
                        child.GetComponent<Image>().sprite = c.image;
                        break;
                    case "Type":
                        child.GetComponent<TextMeshProUGUI>().text = c.type;
                        break;
                    case "Description":
                        child.GetComponent<TextMeshProUGUI>().text = c.description;
                        break;
                    case "Stats":
                            child.GetComponent<TextMeshProUGUI>().text = null;
                        break;
                }
            }

            cardNumber++;
        }

    }
}
