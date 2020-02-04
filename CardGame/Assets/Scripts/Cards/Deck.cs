using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Deck
{
    public List<DeckFormat> cardsInDeck;

    public Deck() { }

    public Deck(List<DeckFormat> cardsInDeck)
    {
        this.cardsInDeck = cardsInDeck;
    }

    public GameObject pullCard()
    {
        int rand = Random.Range(0,cardsInDeck.Count);
        DeckFormat pulledCard = cardsInDeck[rand];
        if (pulledCard.count <= 1)
        {
            cardsInDeck.RemoveAt(rand);
        }
        else
        {
            pulledCard.count--;
        }
        Card card = Resources.Load<Card>("Cards/CreatedCards/" + pulledCard.name);
        return card.spawnCard();
    }
}
