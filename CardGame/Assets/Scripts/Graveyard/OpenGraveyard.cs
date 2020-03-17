using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenGraveyard : MonoBehaviour, IPointerClickHandler
{
    public GameObject cardPrefab;               // Card prefab.
    public Sprite minionBackground;             // Minion card background.
    public Sprite spellBackground;              // Spell card background.

    public List<string> graveyardCards;         // List of cards in graveyard.
    public RectTransform content;               // Parent for cards in graveyard

    public Canvas cardsInGraveyard;             // Canvas for cards in graveyard

    // Start is called before the first frame update
    void Start()
    {
        graveyardCards = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Display cards in graveyard.
    private void displayCardsInGraveyard()
    {
        //destroys all children of content object
        foreach (Transform child in content)
            Destroy(child.gameObject);

        List<Card> cards = new List<Card>();
        //gets all cards in deck as card objects
        foreach (var c in graveyardCards)
            cards.Add(Resources.Load<Card>("Cards/CreatedCards/" + c));

        //calculates height of content game object
        float rows = (float)Math.Ceiling(cards.Count / 5f);
        float height = rows == 1 ? 24 * 2 + 455 : (24 * rows + 24) + (455 * rows);
        content.sizeDelta = new Vector2(0, height);

        foreach (var c in cards)
        {
            //picks background according to card type
            if (c.type.ToLower().Contains("spell"))
                c.spawnCard(cardPrefab, spellBackground, content.gameObject, 0, 0);     //spawns spell
            else
                c.spawnCard(cardPrefab, minionBackground, content.gameObject, 0, 0);    //spawns minion
        }
    }

    // Opens graveyard.
    public void OnPointerClick(PointerEventData eventData)
    {
        displayCardsInGraveyard();
        cardsInGraveyard.gameObject.SetActive(true);
    }
}
