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

    public override string ToString()
    {
        if (cardsInDeck == null)
        {
            return "error";
        }

        string tmp = null;

        foreach(DeckFormat deckFormat in cardsInDeck)
        {
            for (int i = 0; i < deckFormat.count; i++)
            {
                if (string.IsNullOrEmpty(tmp))
                {
                    tmp = deckFormat.name;
                }
                else
                {
                    tmp += "," + deckFormat.name;
                }
            }
        }
        return tmp;
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
