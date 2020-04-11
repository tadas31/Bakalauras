using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicGameManager : MonoBehaviour
{
    public static ClassicGameManager instance;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static PlayerManager curPlayer;

    public GameObject localPlayerPref;
    public GameObject enemyPlayerPref;
    public GameObject handCanvas;
    private void Awake()
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

    public void SpawnPlayer(int _id, string _username)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPref);
            curPlayer = _player.GetComponent<PlayerManager>() ;
        }
        else
        {
            _player = Instantiate(enemyPlayerPref);
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;

        players.Add(_id, _player.GetComponent<PlayerManager>());
    }

    public void PullStartingCards(string _deck)
    {
        string[] values = _deck.Split(',');
        foreach (string value in values)
        {
            AddCardToHand(value);
        }
    }

    public void SetTurn(bool _isTurn)
    {
        curPlayer.isTurn = _isTurn;
    }

    public void AddCardToHand(string cardName)
    {
        Card card = Resources.Load<Card>("Cards/CreatedCards/" + cardName);
        GameObject cardHand = card.spawnCard();
        //GameObject addedCard = Instantiate(card,new Vector3(handCards.transform.position.x, handCards.transform.position.y, handCards.transform.position.z), Quaternion.identity);
        cardHand.transform.localScale = handCanvas.transform.localScale;
        cardHand.AddComponent<CardInHand>();
        cardHand.transform.SetParent(handCanvas.transform);
        handReorganize();
    }

    /// <summary>
    /// Reorganizes the had by the cards in hand
    /// </summary>
    public void handReorganize()
    {
        //Spacing between cards
        float spacing = 100f;
        //The count of cards in the hand
        int count = handCanvas.transform.childCount;
        //Calculation for the card place
        float positionX = -(spacing * (count / 2));
        foreach (RectTransform item in handCanvas.transform)
        {
            item.localPosition = new Vector3(positionX, item.localPosition.y);
            positionX += spacing;
        }
    }
}
