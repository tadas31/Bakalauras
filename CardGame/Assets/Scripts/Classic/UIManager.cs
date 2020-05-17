using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool showConnect = true;
    public float time = 0f;
    public static UIManager instance;

    public GameObject startMenu;
    public GameObject pauze;
    public GameObject timer;
    public GameObject endTurn;
    public TextMeshProUGUI ipAddressField;
    public TextMeshProUGUI usernameField;
    public LoadScene loadScene;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    void Update()
    {
        if (!startMenu.activeSelf && showConnect)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                startMenu.SetActive(true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauze.SetActive(!pauze.activeSelf);
        }
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        time = 1f;
        Client.instance.ConnectToServer();
    }

    public void ActivateTimer()
    {
        timer.SetActive(true);
    }

    public void ActivateEndTurn()
    {
        endTurn.SetActive(true);
    }

    public void OnReturn() 
    {
        pauze.SetActive(!pauze.activeSelf);
    }
    
    public void OnExit()
    {
        loadScene.LoadNewScene("Menu");
    }

    public void EndTurn()
    {
        ClientSend.EndTurn();
    }
}
