using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardCostHelper : MonoBehaviour
{

    public int startingCost;            // Starting cost of card.

    private int cost;                   // Current cost of card.

    private TextMeshProUGUI costText;   // Game object to display cost of the card.

    // Start is called before the first frame update
    void Start()
    {
        costText = transform.GetChild(0).Find("Cost").GetComponent<TextMeshProUGUI>();

        cost = startingCost;
        costText.text = cost.ToString();
    }

    /// <summary>
    /// Removes set amount of cost.
    /// </summary>
    /// <param name="cost"></param>
    public void removeCost(int cost)
    {
        if (this.cost > 0)
        {
            this.cost -= cost;
            costText.text = this.cost.ToString();
        }
    }

    /// <summary>
    /// Sets specific cost to card.
    /// </summary>
    /// <param name="cost"></param>
    public void setCost(int cost)
    {
        if (cost >= 0)
        {
            this.cost = cost;
            costText.text = this.cost.ToString();
        }
    }

    /// <summary>
    /// Gets card cost.
    /// </summary>
    /// <returns></returns>
    public int getCost()
    {
        return cost;
    }
}
