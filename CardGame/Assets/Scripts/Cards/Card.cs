using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "New Card")]
public class Card : ScriptableObject
{
    public string cardName;     //card's name
    public string description;  //card's description
    public int cost;            //card's cost
    public Sprite image;        //card's image
    public string type;         //card's type
    public int attack;          //card's attack
    public int life;            //card's life

    //spawns minion cards
    public void spawnCard(GameObject cardPrefab, Sprite background, GameObject parent, float x, float y)
    {
        GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);    //creates new game object
        newCard.GetComponent<Transform>().SetParent(parent.transform);                      //sets parent
        newCard.transform.localPosition = new Vector3(x, y, 0);                             //sets cards position
        newCard.transform.localScale = Vector3.one;                                         //sets cards scale

        newCard.transform.name = cardName;

        //sets values to all card's fields
        foreach (var child in newCard.GetComponentsInChildren<Transform>())
        {
            switch (child.name)
            {
                case "Background":
                    child.GetComponent<Image>().sprite = background;
                    child.transform.name = "Background - " + cardName;
                    break;
                case "Name":
                    child.GetComponent<TextMeshProUGUI>().text = cardName;
                    break;
                case "Cost":
                    child.GetComponent<TextMeshProUGUI>().text = cost.ToString();
                    break;
                case "Image":
                    child.GetComponent<Image>().sprite = image;
                    break;
                case "Type":
                    child.GetComponent<TextMeshProUGUI>().text = type;
                    break;
                case "Description":
                    child.GetComponent<TextMeshProUGUI>().text = description;
                    break;
                case "Stats":
                    if (type.ToLower() == "spell")
                        child.GetComponent<TextMeshProUGUI>().text = null;
                    else
                        child.GetComponent<TextMeshProUGUI>().text = attack + " / " + life;
                    break;
            }
        }
    }
}
