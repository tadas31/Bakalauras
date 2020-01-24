using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Goblin : AbstractMinion
{
    private GameObject card;

    // Start is called before the first frame update
    void Start()
    {
        title = "Goblin";
        cost = 1;
        image = "Cards/CardImages/goblin_1";
        type = "Minion";
        attack = 1;
        life = 3;
    }

    public override GameObject getCard()
    {

        Debug.Log("HERE");

        card = Resources.Load<GameObject>(CARD_PREFAB_PATH);
        Debug.Log(card.name);

        //card.transform.Find("Background").GetComponent<Image>().sprite = MINION_BACKGROUND;
        //card.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = title;
        //card.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = cost.ToString();
        //card.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(image);
        //card.transform.Find("Type").GetComponent<TextMeshProUGUI>().text = type;
        //card.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = "add all effects if there are no effects leave empty";
        //card.transform.Find("Stats").GetComponent<TextMeshProUGUI>().text = attack + " / " + life;

        return card;
    }



    // Update is called once per frame
    void Update()
    {

    }
}




//public void spawnCard(Card c, float x, float y)
//{
//    GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
//    newCard.GetComponent<Transform>().SetParent(content.transform);
//    newCard.transform.localPosition = new Vector3(x, y, 0);
//    Debug.Log(newCard.transform.localRotation);


//    newCard.transform.localScale = Vector3.one;

//    foreach (var child in newCard.GetComponentsInChildren<Transform>())
//    {
//        switch (child.name)
//        {
//            case "Background":
//                if (c.type.ToLower() != "spell")
//                    child.GetComponent<Image>().sprite = minionBackground;
//                else
//                    child.GetComponent<Image>().sprite = spellBackground;
//                break;
//            case "Title":
//                child.GetComponent<TextMeshProUGUI>().text = c.title;
//                break;
//            case "Cost":
//                child.GetComponent<TextMeshProUGUI>().text = c.cost.ToString();
//                break;
//            case "Image":
//                Sprite image = Resources.Load<Sprite>(c.image);
//                child.GetComponent<Image>().sprite = image;
//                break;
//            case "Type":
//                child.GetComponent<TextMeshProUGUI>().text = c.type;
//                break;
//            case "Description":
//                child.GetComponent<TextMeshProUGUI>().text = c.description;
//                break;
//            case "Stats":
//                if (c.type.ToLower() != "spell")
//                    child.GetComponent<TextMeshProUGUI>().text = c.attack + " / " + c.life;
//                else
//                    child.GetComponent<TextMeshProUGUI>().text = null;
//                break;
//        }
//    }
//}
