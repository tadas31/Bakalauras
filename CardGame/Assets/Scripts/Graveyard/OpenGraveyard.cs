using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenGraveyard : MonoBehaviour, IPointerClickHandler
{
    public Canvas cardsInGraveyard;         // Canvas for cards in graveyard

    // Opens graveyard.
    public void OnPointerClick(PointerEventData eventData)
    {
        cardsInGraveyard.gameObject.SetActive(true);
    }
}
