
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

    [SyncVar]
    public bool isTurn = false;
    [SyncVar]
    public int maxMana = 1;
    [SyncVar]
    public int curMana = 1;
    [SyncVar (hook = "OnTimeChange")]
    public float timer = 60;
    [SyncVar]
    public bool gameStart = false;

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer == false)
        {
            return;
        }
        GameObject.Find("/Canvas/EndTurn").GetComponent<Button>().onClick.AddListener(ChangeTurns);
        Debug.Log("PlayerConnectionObject::Start -- Spawning my own personal unit.");
    }

    public void Update()
    {
        if (!isLocalPlayer) 
        {
            return;
        }

        if (gameStart)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //CmdChangeTurn();
            }
        }
    }

    void OnTimeChange(float time)
    {
        GameObject.Find("/Canvas/Timer").GetComponent<TextMeshProUGUI>().text = time.ToString();
    }
    void ChangeTurns()
    {
        CmdChangeTurn();
    }

    [Command]
    void CmdChangeTurn()
    {
        Debug.Log("Changing turns");
        timer = 60;
        RpcChangeTurns();
    }

    [ClientRpc]
    void RpcChangeTurns()
    {
        Debug.Log("Wtf");
        if (isTurn)
        {
            endTurn();
            isTurn = false;
        }
        else
        {
            startTurn();
            isTurn = true;
        }
    }
    
    public void startTurn() 
    {
        if(maxMana < 10)
        {
            maxMana++;
        }
        curMana = maxMana;
        isTurn = true;
    }

    public void endTurn()
    {
        
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
        localCard.AddComponent<NetworkCard>();
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
            if (localCard.GetComponent<NetworkCard>().hasAuthority)
            {
                GameObject playerBoard = GameObject.Find("Board/PlayerBoard");
                localCard.transform.SetParent(playerBoard.transform);
                localCard.transform.localScale = Vector3.one;
            }
            else
            {
                GameObject enemyBoard = GameObject.Find("Board/EnemyBoard");
                localCard.transform.SetParent(enemyBoard.transform);
                localCard.transform.localScale = Vector3.one;
            }
        }

        // Enables all attached scripts.
        foreach (MonoBehaviour script in gameObject.GetComponents<MonoBehaviour>())
            script.enabled = true;

        
        //Changes the parent of the card to player board.
    }
}
