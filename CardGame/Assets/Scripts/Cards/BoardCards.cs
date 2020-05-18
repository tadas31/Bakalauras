using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCards : MonoBehaviour
{
    public static int attackingCard;
    public static int defendingCard;
    //The spacing between the cards that are put on board.
    private static float SPACING_BETWEEN_CARDS = 340f;
    public void OnTransformChildrenChanged()
    {
        reorganizeBoard();
    }

    /// <summary>
    /// Reorganizes the cards on the board by the number of cards on it.                                                         
    /// </summary>
    private void reorganizeBoard() 
    {
        //The count of cards in the hand
        int count = this.transform.childCount;
        //Calculation for the card place
        float positionX = -(SPACING_BETWEEN_CARDS * (count / 2));
        foreach (RectTransform item in this.transform)
        {
            item.localPosition = new Vector3(positionX, 0);
            positionX += SPACING_BETWEEN_CARDS;
        }
    }
}
