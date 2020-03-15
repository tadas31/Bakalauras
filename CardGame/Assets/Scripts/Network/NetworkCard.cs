using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkCard : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerBoard = GameObject.Find("Board/PlayerBoard");
        this.transform.SetParent(playerBoard.transform);
        this.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority == false)
        {
            return;
        }
    }

    [Command]
    public void CmdPutOnTable()
    {
        RpcPutOnTable();
    }

    [ClientRpc]
    void RpcPutOnTable() 
    {
        //Changes the parent of the card to player board.
        GameObject playerBoard = GameObject.Find("Board/PlayerBoard");
        this.transform.SetParent(playerBoard.transform);
        this.transform.localScale = Vector3.one;
    }
}
