using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnCardDestroy : MonoBehaviour
{
    private OpenGraveyard graveyard;        // Reference to Graveyard script

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // Add card to graveyard.
    private void OnDestroy()
    {
        graveyard = GameObject.Find("Graveyard").GetComponent<OpenGraveyard>();
        graveyard.graveyardCards.Add(transform.GetChild(0).Find("Name").GetComponent<TextMeshProUGUI>().text);
    }
}
