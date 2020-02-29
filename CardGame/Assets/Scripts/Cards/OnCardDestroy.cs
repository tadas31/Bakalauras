using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnCardDestroy : MonoBehaviour
{
    private OpenGraveyard graveyard;        // Reference to Graveyard script

    private void Awake()
    {
        graveyard = GameObject.Find("Graveyard").GetComponent<OpenGraveyard>();
    }

    // Add card to graveyard.
    private void OnDestroy()
    {
        graveyard.graveyardCards.Add(transform.GetChild(0).Find("Name").GetComponent<TextMeshProUGUI>().text);
    }
}
