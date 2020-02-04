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

    /// <summary>
    /// Spawns the card.
    /// </summary>
    /// <returns>Returns the game object of the card.</returns>
    public GameObject spawnCard() 
    {
        //Getting the main card prefab.
        GameObject cardPrefab = Resources.Load<GameObject>("Cards/Prefabs/Card"); ;
        //Getting the background of the card by type.
        Sprite background = getBackgroundSprite(type);
        //Creates the game object of the card.
        GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
        //Sets all of the parameters of the card.
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
        return newCard;
    }



    //spawns card with all data about it
    public GameObject spawnCard(GameObject cardPrefab, Sprite background, GameObject parent, float x, float y)
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
        return newCard;
    }

    //spawns card only displaying cost name and amount of copies in deck
    public void spawnCardCompact(GameObject cardPrefab, Sprite background, GameObject parent, int amountInDeck, float x, float y)
    {
        GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);    //creates new game object
        newCard.GetComponent<Transform>().SetParent(parent.transform);                      //sets parent
        newCard.transform.localPosition = new Vector3(x, y, 0);                             //sets cards position
        newCard.transform.localScale = Vector3.one;                                         //sets cards scale

        //sets values to fields
        newCard.transform.name = "CardInDeck - " + cardName;
        newCard.GetComponent<Image>().sprite = background;
        newCard.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = cost.ToString();
        newCard.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = cardName;
        newCard.transform.Find("AmountInDeck").GetComponent<TextMeshProUGUI>().text = "x" + amountInDeck.ToString();
    }
    /// <summary>
    /// Gets the background sprite by type.
    /// </summary>
    /// <param name="type">Type of the card.</param>
    /// <returns>Sprite of the background.</returns>
    public Sprite getBackgroundSprite(string type)
    {
        //If it is a spell card type returns spell background.
        if (type.ToLower().Contains("spell"))
        {
            return Resources.Load<Sprite>("Cards/Backgrounds/Spell");
        }
        //If there are no that meet the criteria returns the minion sprite.
        return Resources.Load<Sprite>("Cards/Backgrounds/Minion");
    }
}
