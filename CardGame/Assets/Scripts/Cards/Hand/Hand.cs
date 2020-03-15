using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Hand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        pullStartingHand();
    }

    public void pullStartingHand()
    { 
        Deck deck = SaveSystem.LoadDeck();
        for (int i = 0; i < 8; i++)
        {
            addCardToHand(deck.pullCard());
        }
    }
    //Adds card to the hand of the player
    public void addCardToHand(GameObject addedCard)
    {
        //GameObject addedCard = Instantiate(card,new Vector3(handCards.transform.position.x, handCards.transform.position.y, handCards.transform.position.z), Quaternion.identity);
        addedCard.transform.localScale = this.transform.localScale;
        addedCard.AddComponent<CardInHand>();
        addedCard.transform.SetParent(this.transform);
        handReorganize();
    }
    /// <summary>
    /// Reorganizes the had by the cards in hand
    /// </summary>
    public void handReorganize()
    {
        //Spacing between cards
        float spacing = 100f;
        //The count of cards in the hand
        int count = this.transform.childCount;
        //Calculation for the card place
        float positionX = -(spacing * (count / 2));
        foreach (RectTransform item in this.transform)
        {
            item.localPosition = new Vector3(positionX, item.localPosition.y);
            positionX += spacing;
        }
    }
}
