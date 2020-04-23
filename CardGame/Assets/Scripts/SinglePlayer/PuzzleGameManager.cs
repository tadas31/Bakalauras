using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PuzzleGameManager : MonoBehaviour
{
    public static PuzzleGameManager instance;

    public GameObject handCards;
    //public GameObject card;

    public GameObject pauze;

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

    // Start is called before the first frame update
    void Start()
    {
        Deck deck = SaveSystem.LoadDeck();
        for (int i = 0; i < 8; i++)
        {
            addCardToHand(deck.pullCard());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauze.SetActive(!pauze.activeSelf);
        }
    }



    //Adds card to the hand of the player
    public void addCardToHand(GameObject addedCard) 
    {
        addedCard.transform.localScale = handCards.transform.localScale;
        addedCard.AddComponent<CardInHand>();
        addedCard.transform.SetParent(handCards.transform);
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

    public void onResumePress()
    {
        pauze.SetActive(false);
    }

    public void PutOnTable(GameObject card, bool duplicate = false)
    {
        
        //Debug.Log(card);
        if (card.transform.GetChild(0).Find("Type").GetComponent<TextMeshProUGUI>().text.ToLower().Contains("spell"))
        {
            //Changes the parent of the card to spells.
            GameObject spells = GameObject.Find("Spells");
            card.transform.SetParent(spells.transform);
            card.transform.localScale = Vector3.one;
        }
        else
        {
            //Changes the parent of the card to player board.
            GameObject playerBoard = GameObject.Find("Board/PlayerBoard");
            card.transform.SetParent(playerBoard.transform);
            card.transform.localScale = Vector3.one;

            // Spawns duplicate if card has this effect
            if (duplicate)
            {
                GameObject c = Instantiate(card);
                c.name = c.name.Replace("(Clone)", "");
                c.transform.SetParent(playerBoard.transform);
                c.transform.localScale = Vector3.one;
                Destroy(c.GetComponent<CardInHand>());

                //Enables all attached scripts.
                foreach (MonoBehaviour script in c.GetComponents<MonoBehaviour>())
                    script.enabled = true;
            }
                
        }

        //Enables all attached scripts.
        foreach (MonoBehaviour script in card.GetComponents<MonoBehaviour>())
            script.enabled = true;
    }
}
