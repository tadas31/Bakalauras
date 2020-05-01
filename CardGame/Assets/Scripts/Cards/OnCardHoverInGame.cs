using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnCardHoverInGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject bigCard;                 // Copy of card that is being hovered over.
    private Transform displayHoveredCard;       // Parent of hovered card.
    // Start is called before the first frame update
    void Start()
    {
        displayHoveredCard = GameObject.Find("Canvas/DisplayHoverCard").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Disables all helper scripts for hovered card.
        foreach (MonoBehaviour script in gameObject.GetComponents<MonoBehaviour>())
        {
            if (script.GetType().Name.ToLower().Contains("helper"))
                script.enabled = false;
        }

        //Creates copy of card that is being hovered over.
        bigCard = Instantiate(gameObject);

        Destroy(bigCard.transform.GetChild(0).transform.Find("Image/DamageTaken").gameObject);
        Destroy(bigCard.transform.GetChild(0).transform.Find("Image/AttackHealthRecieved").gameObject);

        // Enable helper scripts for hovered card.
        foreach (MonoBehaviour script in gameObject.GetComponents<MonoBehaviour>())
        {
            if (script.GetType().Name.ToLower().Contains("helper"))
                script.enabled = true;
        }

        // Disables all scripts for big card.
        foreach (MonoBehaviour script in bigCard.GetComponents<MonoBehaviour>())
        {
            script.enabled = false;
        }

        // Sets parent and position for big card
        bigCard.transform.SetParent(displayHoveredCard);
        RectTransform bigCardRectTransform = bigCard.GetComponent<RectTransform>();
        bigCardRectTransform.anchorMin = Vector2.zero;
        bigCardRectTransform.anchorMax = Vector2.zero;
        bigCardRectTransform.anchoredPosition = new Vector2(-705, 50);
        bigCard.transform.localScale = Vector3.one;
        bigCardRectTransform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        for (int i = 0; i < displayHoveredCard.childCount; i++)
            Destroy(displayHoveredCard.GetChild(i).gameObject);
    }
}
