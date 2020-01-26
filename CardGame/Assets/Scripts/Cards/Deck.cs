using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Deck
{
    public List<string> cardsInDeck;

    public Deck(List<string> cardsInDeck)
    {
        this.cardsInDeck = cardsInDeck;
    }
}
