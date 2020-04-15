using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : MonoBehaviour
{
    public static EnemyHand instance;
    public GameObject cardDown;
    int cardCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }


    public void SetCardCount(int count)
    {
        if (count > cardCount) 
        {
            int diffrence = count - cardCount;
            for (int i = 0; i < diffrence; i++)
            {
                AddCard();
            }
        } 
        else if (count < cardCount)
        {
            int diffrence = cardCount - count;
            for (int i = 0; i < diffrence; i++)
            {
                RemoveCard();
            }
        }
        HandReorganize();
        cardCount = count;
    }

    public void AddCard()
    {
        GameObject card = Instantiate(cardDown, this.transform);
    }

    public void RemoveCard()
    {
        Destroy(this.transform.GetChild(0).gameObject);
    }

    /// <summary>
    /// Reorganizes the had by the cards in hand
    /// </summary>
    public void HandReorganize()
    {
        //Spacing between cards
        float spacing = 60f;
        //The count of cards in the hand
        int count = transform.childCount;
        //Calculation for the card place
        float positionX = -(spacing * (count / 2));
        foreach (RectTransform item in transform)
        {
            item.localPosition = new Vector3(positionX, item.localPosition.y);
            positionX += spacing;
        }
    }
}
