using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectCards : MonoBehaviour
{
    public RectTransform content;                   //parent for cards 
    public TextMeshProUGUI amountOfCardsInDeck;     //amount of cards in deck

    private GraphicRaycaster raycaster;
    private bool locker = false;                    //if true user has pressed left mouse button on card but haven't released yet


    private List<RaycastResult> results;            //objects hit by raycast when user pressed left mouse button
    private List<RaycastResult> resultsRelease;     //objects hit by raycast when user released left mouse button
    private float startPos;                         //position of content game object when user pressed left mouse button
    private float endPos;                           //position of content game object when user released left mouse button

    private List<string> cardsInDeck;               //list of cards in deck


    // Start is called before the first frame update
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();

        cardsInDeck = SaveSystem.LoadDeck() == null ? new List<string>() : SaveSystem.LoadDeck().cardsInDeck;   //gets cards from save file if file does not exists sets value to nu;;
    }

    // Update is called once per frame
    void Update()
    {
        amountOfCardsInDeck.text = cardsInDeck.Count + "/30 cards"; //displays amount of cards in deck

        //adds selected cards to save file
        GameObject c = selectCard();
        if (c != null && !c.gameObject.name.Contains("CardInDeck - "))
        {
            cardsInDeck.Add(c.name.Replace("Background - ", "") );
            SaveSystem.SaveDeck(cardsInDeck);
        }
            
    }


    //gets card that is under mouse cursor
    private GameObject selectCard()
    {
        if (!locker)
        {
            results = new List<RaycastResult>();
            resultsRelease = new List<RaycastResult>();
            startPos = 0;
            endPos = 0;
        }

        //casts ray when mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {

            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;
            raycaster.Raycast(pointerData, results);

            startPos = content.transform.position.y;

            locker = true;
        }

        //casts ray when mouse button is released and compares results if they match returns selected card
        if (Input.GetMouseButtonUp(0))
        {
            PointerEventData pointerDataRelease = new PointerEventData(EventSystem.current);
            pointerDataRelease.position = Input.mousePosition;

            raycaster.Raycast(pointerDataRelease, resultsRelease);

            endPos = content.transform.position.y;

            GameObject card = null;
            GameObject cardRelease = null;
            string inSelectionOrDeck = null;


            foreach (RaycastResult result in results)
            {
                if (result.gameObject.name.Contains("Background - ") || result.gameObject.name.Contains("CardInDeck - "))
                {
                    card = result.gameObject;
                    inSelectionOrDeck = card.name.Split(' ')[0];
                }
                   
            }

            foreach (RaycastResult result in resultsRelease)
            { 
                if (result.gameObject.name.Contains(inSelectionOrDeck + " - "))
                    cardRelease = result.gameObject;
            }

            locker = false;


            if (card == cardRelease && startPos == endPos && cardRelease != null)
                return cardRelease;
        }

        return null;
    }
}
