
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;

/// <summary>
/// Add commetns
/// </summary>

public class  NetworkPlayer : NetworkBehaviour
{
    public GameObject handCanvas;
    public GameObject card;
    public bool state = true;

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer == false)
        {
            return;
        }
        Debug.Log("PlayerConnectionObject::Start -- Spawning my own personal unit.");
    }

    private void Update()
    {
        if (isLocalPlayer == false)
        {
            return;
        }
    }

    public void spawnGameObject(string name)
    {
        if (isLocalPlayer)
        {
            CmdSpawnGameObject(name);
        }
    }


    [Command]
    void CmdSpawnGameObject(string name)
    {
        GameObject obj = Instantiate(card);
        NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
        NetworkInstanceId id = obj.GetComponent<NetworkIdentity>().netId;
        RpcSpawnGameObject(id, name);
    }

    [ClientRpc]
    void RpcSpawnGameObject(NetworkInstanceId id, string name)
    { 
        GameObject localCard = ClientScene.FindLocalObject(id);
        Debug.Log(localCard);
        
        List<Card> cards = Resources.LoadAll<Card>("Cards/CreatedCards").ToList();
        //Get the needed card;
        Card selected = new Card();
        foreach (Card item in cards)
        {
            if (item.cardName == name)
            {
                selected = item;
                break;
            }
        }
        selected.AddElementsToCard(localCard);
        localCard.AddComponent<NetworkCard>();
        //Changes the parent of the card to player board.
        GameObject playerBoard = GameObject.Find("Board/PlayerBoard");
        localCard.transform.SetParent(playerBoard.transform);
        localCard.transform.localScale = Vector3.one;
    }

    [Command]
    void CmdSpawnMyUnit()
    {
        // We are guaranteed to be on the server right now.
        GameObject go = Instantiate(handCanvas);
        

        // Now that the object exists on the server, propagate it to all
        // the clients (and also wire up the NetworkIdentity)
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

}
