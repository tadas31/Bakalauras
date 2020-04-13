using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseGraveyard : MonoBehaviour, IPointerClickHandler
{
    public GameObject garveyard;     // Gets graveyard window

    // Closes graveyard
    public void OnPointerClick(PointerEventData eventData)
    {
        garveyard.SetActive(false);
    }
}
