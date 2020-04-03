
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

//Add comments
class NetworkManager : UnityEngine.Networking.NetworkManager
{
    List<NetworkConnection> networkConnections = new List<NetworkConnection>();
    public bool isFliped = false;
    public static NetworkManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    void Update()
    {
        if(networkConnections.Count >= 2 && !Instance.isFliped)
        {
            Instance.coinFlip();
        }
    }
    // Server callbacks
    public override void OnServerConnect(NetworkConnection conn)
    {
        networkConnections.Add(conn);
    }

    public void coinFlip()
    {
        isFliped = true;
        int rand  = UnityEngine.Random.Range(0,2);
        Debug.Log(rand);
        //Sets the randoms player to start
        Instance.networkConnections[rand].playerControllers[0].gameObject.GetComponent<NetworkPlayer>().isTurn = true;
        Instance.networkConnections[0].playerControllers[0].gameObject.GetComponent<NetworkPlayer>().gameStart = true;
        Instance.networkConnections[1].playerControllers[0].gameObject.GetComponent<NetworkPlayer>().gameStart = true;
    }
}

