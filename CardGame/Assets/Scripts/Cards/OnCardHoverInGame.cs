using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
        // Creates copy of card that is being hovered over.
        //bigCard = Instantiate(gameObject);
        //bigCard.transform.SetParent(displayHoveredCard);
        //bigCard.transform.position = Vector3.zero;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Destroys card that is being hovered over.
        //Destroy(bigCard);
    }
}
