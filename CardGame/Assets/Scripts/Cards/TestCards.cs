//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class TestCards : MonoBehaviour
//{
//    public GameObject canvas;
//    public GameObject card;
//    public Sprite minionBackground;
//    public Sprite spellBackground;


//    private AllCards cards;


//    // Start is called before the first frame update
//    void Start()
//    {
//        cards = new AllCards();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.M))
//        {
//            Card c = cards.getCard("minionCard");
//            Debug.Log(string.Format(" title - {0}\n cost - {1}\n type - {2}\n description - {3}\n image - {4}\n attack - {5}\n life - {6}\n", 
//                c.title, c.cost, c.type, c.description, c.image, c.attack, c.life));

//            spawnCard(c);
//        }

//        if (Input.GetKeyDown(KeyCode.S))
//        {
//            Card c = cards.getCard("spellCard");
//            Debug.Log(string.Format(" title - {0}\n cost - {1}\n type - {2}\n description - {3}\n image - {4}\n attack - {5}\n life - {6}\n",
//                c.title, c.cost, c.type, c.description, c.image, c.attack, c.life));

//            spawnCard(c);
//        }
//    }

//    public void spawnCard(Card c)
//    {
//        GameObject newCard = Instantiate(card, Vector3.zero, Quaternion.identity);
//        newCard.GetComponent<Transform>().SetParent(canvas.transform);
//        newCard.transform.localPosition = Vector3.zero;

//        foreach (var child in newCard.GetComponentsInChildren<Transform>())
//        {
//            switch (child.name)
//            {
//                case "Background":
//                    if (c.type.ToLower() != "spell")
//                        child.GetComponent<Image>().sprite = minionBackground;
//                    else
//                        child.GetComponent<Image>().sprite = spellBackground;
//                    break;
//                case "Title":
//                    child.GetComponent<TextMeshProUGUI>().text = c.title;
//                    Debug.Log("HERE");
//                    break;
//                case "Cost":
//                    child.GetComponent<TextMeshProUGUI>().text = c.cost.ToString();
//                    break;
//                case "Image":
//                    Sprite image = Resources.Load<Sprite>(c.image);
//                    Debug.Log(image.name);
//                    child.GetComponent<Image>().sprite = image;
//                    break;
//                case "Type":
//                    child.GetComponent<TextMeshProUGUI>().text = c.type;
//                    break;
//                case "Description":
//                    child.GetComponent<TextMeshProUGUI>().text = c.description;
//                    break;
//                case "Stats":
//                    if (c.type.ToLower() != "spell")
//                        child.GetComponent<TextMeshProUGUI>().text = c.attack + " / " + c.life;
//                    else
//                        child.GetComponent<TextMeshProUGUI>().text = null;
//                    break;
//            }
//        }
//    }
//}
