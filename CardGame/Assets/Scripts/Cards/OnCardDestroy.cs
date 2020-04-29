using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnCardDestroy : MonoBehaviour
{
    private OpenGraveyard graveyard;        // Reference to Graveyard script
    private Transform parent;               // Parent of card;

    private void Start()
    {
        graveyard = GameObject.Find("Graveyard").GetComponent<OpenGraveyard>();
        parent = transform.parent;
    }

    // Add card to graveyard.
    private void OnDestroy()
    {
        if (parent != null && ( parent.name.Contains("Hand") || parent.name.Contains("Player") ))
            graveyard.graveyardCards.Add(transform.GetChild(0).Find("Name").GetComponent<TextMeshProUGUI>().text);
        
        if (ClassicGameManager.instance != null)
        {
            ClassicGameManager.instance.HandReorganize();
        }

        OpenGraveyard.instance.UpdateCardNumberInGraveyard();
    }
}
