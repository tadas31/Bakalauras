
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;
using TMPro;

/// <summary>
/// Add commetns
/// </summary>

public class  NetworkPlayer : NetworkBehaviour
{
    public GameObject playerBoard;
    public GameObject card;

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer == false)
        {
            return;
        }
        Debug.Log("PlayerConnectionObject::Start -- Spawning my own personal unit.");
        CmdSpawnPlayerBoard();
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

        if (localCard.transform.GetChild(0).Find("Type").GetComponent<TextMeshProUGUI>().text.ToLower().Contains("spell"))
        {
            //Changes the parent of the card to spells.
            GameObject spells = GameObject.Find("Spells");
            localCard.transform.SetParent(spells.transform);
            localCard.transform.localScale = Vector3.one;
        }
        else
        {
            //Changes the parent of the card to player board.
            GameObject playerBoard = GameObject.Find("Board/PlayerBoard");
            localCard.transform.SetParent(playerBoard.transform);
            localCard.transform.localScale = Vector3.one;
        }

        // Enables all attached scripts.
        foreach (MonoBehaviour script in gameObject.GetComponents<MonoBehaviour>())
            script.enabled = true;

        localCard.AddComponent<NetworkCard>();
        //Changes the parent of the card to player board.
    }

    [Command]
    void CmdSpawnPlayerBoard()
    {
        // We are guaranteed to be on the server right now.
        GameObject go = Instantiate(playerBoard);
        if(FindObjectOfType<NetworkManager>().numPlayers > 1)
        {
            Debug.Log("Second added");
        }
        

        // Now that the object exists on the server, propagate it to all
        // the clients (and also wire up the NetworkIdentity)
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

}
