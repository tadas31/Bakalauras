using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Minion", menuName = "Minion card")]
public class Minion : ScriptableObject
{
    public string cardName;     //card's name
    public string description;  //card's description
    public int cost;            //card's cost
    public Sprite image;        //card's image
    public string type;         //card's type
    public int attack;          //minion's attack
    public int life;            //minion's life

    //spawns minion cards
    public void spawnCard(GameObject cardPrefab, Sprite background, GameObject parrent, Minion card, float x, float y)
    {
        GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
        newCard.GetComponent<Transform>().SetParent(parrent.transform);
        newCard.transform.localPosition = new Vector3(x, y, 0);

        newCard.transform.localScale = Vector3.one;

        foreach (var child in newCard.GetComponentsInChildren<Transform>())
        {
            switch (child.name)
            {
                case "Background":
                    child.GetComponent<Image>().sprite = background;
                    break;
                case "Title":
                    child.GetComponent<TextMeshProUGUI>().text = card.cardName;
                    break;
                case "Cost":
                    child.GetComponent<TextMeshProUGUI>().text = card.cost.ToString();
                    break;
                case "Image":
                    child.GetComponent<Image>().sprite = card.image;
                    break;
                case "Type":
                    child.GetComponent<TextMeshProUGUI>().text = card.type;
                    break;
                case "Description":
                    child.GetComponent<TextMeshProUGUI>().text = card.description;
                    break;
                case "Stats":
                    child.GetComponent<TextMeshProUGUI>().text = card.attack + " / " + card.life;
                    break;
            }
        }
    }
}    