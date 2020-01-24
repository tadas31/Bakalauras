using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCard : MonoBehaviour
{
    public string CARD_PREFAB_PATH; /*= "Cards/Prefabs/Card";*/

    public string title { get; set; }       //card's title
    public int cost { get; set; }           //card's cost
    public string image { get; set; }       //card's image
    public string type { get; set; }        //card's type

    public abstract GameObject getCard();
}
