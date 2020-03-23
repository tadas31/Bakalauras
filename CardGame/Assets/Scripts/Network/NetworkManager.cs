
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
    [SyncVar]
    public float time = 60;

    public static NetworkManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    [Server]
    private void Update()
    {
        if (time < 0)
        {
            Debug.Log("Turn ends");
            return;
        }

        time -= Time.deltaTime;
        RpcUpdateTime(time);
    }

    [ClientRpc]
    public void RpcUpdateTime(float curtime)
    {
        GameObject timerText = GameObject.Find("Canvas/Timer");
        timerText.GetComponent<TextMeshProUGUI>().text = Mathf.Round(curtime).ToString();
    }
}

