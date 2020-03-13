
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

/// <summary>
/// Add commetns
/// </summary>

public class  NetworkPlayer : NetworkBehaviour
{
    public GameObject handCanvas;
    public GameObject handCards;
    public bool state = true;

    // Use this for initialization
    void Start()
    {
 
        if (isLocalPlayer == false)
        {
            return;
        }


        Debug.Log("PlayerConnectionObject::Start -- Spawning my own personal unit.");

        RpcSpawnMyUnit();
    }

    private void Update()
    {
        if (isLocalPlayer == false)
        {
            return;
        }

        if (handCards != null && state)
        {
            state = false;
            Deck deck = SaveSystem.LoadDeck();
            for (int i = 0; i < 8; i++)
            {
                RpcAddCardToHand(deck.pullCard());
            }
           Debug.Log("Put the cards to the hand");
        }
    }

    [ClientRpc]
    void RpcSpawnMyUnit()
    {
        // We are guaranteed to be on the server right now.
        GameObject go = Instantiate(handCanvas);
        handCards = go;
        Deck deck = SaveSystem.LoadDeck();
        for (int i = 0; i < 8; i++)
        {
            Debug.Log("Adding card");
            RpcAddCardToHand(deck.pullCard());
        }

        

        // Now that the object exists on the server, propagate it to all
        // the clients (and also wire up the NetworkIdentity)
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }


    
    //Adds card to the hand of the player
    public void RpcAddCardToHand(GameObject addedCard)
    {
        //GameObject addedCard = Instantiate(card,new Vector3(handCards.transform.position.x, handCards.transform.position.y, handCards.transform.position.z), Quaternion.identity);
        addedCard.transform.localScale = handCards.transform.localScale;
        addedCard.AddComponent<CardInHand>();
        addedCard.transform.SetParent(handCards.transform);
        RpcHandReorganize();
    }

    /// <summary>
    /// Reorganizes the had by the cards in hand
    /// </summary>
    public void RpcHandReorganize()
    {
        //Spacing between cards
        float spacing = 100f;
        //The count of cards in the hand
        int count = handCards.transform.childCount;
        //Calculation for the card place
        float positionX = -(spacing * (count / 2));
        foreach (RectTransform item in handCards.transform)
        {
            item.localPosition = new Vector3(positionX, item.localPosition.y);
            positionX += spacing;
        }
    }
}
