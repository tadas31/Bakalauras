using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectCards : MonoBehaviour
{
    public RectTransform content;
    public TextMeshProUGUI amountOfCardsInDeck;

    private GraphicRaycaster raycaster;
    private bool locker = false;


    private List<RaycastResult> results;
    private List<RaycastResult> resultsRelease;
    private float startPos;
    private float endPos;

    private List<string> cardsInDeck;


    // Start is called before the first frame update
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();

        cardsInDeck = SaveSystem.LoadDeck() == null ? new List<string>() : SaveSystem.LoadDeck().cardsInDeck;
    }

    // Update is called once per frame
    void Update()
    {
        amountOfCardsInDeck.text = cardsInDeck.Count + "/30 cards";

        GameObject c = selectCard();
        if (c != null)
        {
            cardsInDeck.Add(c.name.Replace("Background - ", "") );
            Debug.Log(c.name);
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveSystem.SaveDeck(cardsInDeck);
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach(var card in cardsInDeck)
            {
                Debug.Log(card);
            }
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

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.name.Contains("Background - "))
                    card = result.gameObject;
            }

            foreach (RaycastResult result in resultsRelease)
            {
                if (result.gameObject.name.Contains("Background - "))
                    cardRelease = result.gameObject;
            }

            locker = false;


            if (card == cardRelease && startPos == endPos && cardRelease != null)
                return cardRelease;
        }

        return null;
    }
}
