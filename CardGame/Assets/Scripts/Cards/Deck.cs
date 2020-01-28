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
}
