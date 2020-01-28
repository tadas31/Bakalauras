using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject handCards;
    public GameObject card;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            addCardToHand();
        }

    }

    // Update is called once per frame
    void Update()
    {
    }



    //Adds card to the hand of the player
    public void addCardToHand() 
    {
        GameObject addedCard = Instantiate(card,new Vector3(handCards.transform.position.x, handCards.transform.position.y, handCards.transform.position.z), Quaternion.identity);
        addedCard.transform.localScale = handCards.transform.localScale;
        addedCard.transform.SetParent(handCards.transform);
        Debug.Log("Card added to the hand");
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
        int count = handCards.transform.childCount;
        //Calculation for the card place
        float positionX = -(spacing * (count / 2));
        foreach (RectTransform item in handCards.transform)
        {
            item.localPosition = new Vector3 (positionX,item.localPosition.y);
            positionX += spacing;
        }
    }
}
