using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCardsInDeck : MonoBehaviour
{
    public RectTransform content;           //parent for cards 
    public GameObject compactCardPrefab;    //card prefab
    public Sprite compactBackground;        //card background

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //displays cards in deck
    public void updateCardDisplay(List<DeckFormat> cardsInDeck)
    {
        List<Card> cards = new List<Card>();

        //gets all cards in deck as card objects
        foreach (var c in cardsInDeck)
            cards.Add(Resources.Load<Card>("Cards/CreatedCards/" + c.name));

        //calculates height of content game object
        float rows = cards.Count;
        float height = rows == 1 ? 24 * 2 + 60 : (10 * rows + 24) + (60 * rows) + 14;

        //destroys all children of content object
        foreach (Transform child in content)
            Destroy(child.gameObject);

        content.sizeDelta = new Vector2(0, height); //sets height of content game object

        for (int i = 0; i < cards.Count; i++)
        {
            // Spawns card
            cards[i].spawnCardCompact(compactCardPrefab, compactBackground, content.gameObject, cardsInDeck[i].count, 0, 0);
        }
    }


}
