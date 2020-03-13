
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
        CmdSpawnMyUnit();
    }

    private void Update()
    {
        if (isLocalPlayer == false)
        {
            return;
        }
    }

    [Command]
    void CmdSpawnMyUnit()
    {
        // We are guaranteed to be on the server right now.
        GameObject go = Instantiate(handCanvas);
        handCards = go;
        

        // Now that the object exists on the server, propagate it to all
        // the clients (and also wire up the NetworkIdentity)
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

}
