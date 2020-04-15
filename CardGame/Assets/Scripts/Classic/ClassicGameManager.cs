using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicGameManager : MonoBehaviour
{
    public static ClassicGameManager instance;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static PlayerManager curPlayer;

    public GameObject localPlayer;
    public GameObject enemyPlayer;
    public GameObject handCanvas;
    //TODO: Maybe change that the localPLayer and enemy prefabs would be spawned as board elements.
    public GameObject playerBoard;
    public GameObject enemyBoard;
    public GameObject pauze;
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
            _player = localPlayer;
            curPlayer = _player.GetComponent<PlayerManager>() ;
        }
        else
        {
            _player = enemyPlayer;
            GameObject.Find("Canvas/EndTurn").SetActive(true);
            GameObject.Find("Canvas/Timer").SetActive(true);
        }

        _player.SetActive(true);
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

    public void SetLife(int _clientId, int _amount)
    {
        foreach (PlayerManager item in players.Values)
        {
            if (_clientId == item.id)
            {
                item.life = _amount;
            }
        }
    }

    public void SetMana(int _clientId, int _amount)
    {
        foreach (PlayerManager item in players.Values)
        {
            if (_clientId == item.id)
            {
                item.mana = _amount;
            }
        }
    }

    public void Attack(int _clientFrom, string _from, int _fromLife, string _to, int _toLife)
    {
        Transform attackingCard;
        Transform defendingCard;
        Debug.Log(_from);
        if (Client.instance.myId == _clientFrom)
        {
            attackingCard = playerBoard.transform.Find(_from);
            defendingCard = enemyBoard.transform.Find(_to);
        }
        else
        {
            attackingCard = enemyBoard.transform.Find(_from);
            defendingCard = playerBoard.transform.Find(_to);
        }
        Debug.Log(attackingCard);
        Debug.Log(defendingCard);

        attackingCard.GetComponent<Attack>().AttackAnimation(defendingCard);
    }

    public void PutOnTable(string _cardName, bool _isPlayers)
    {
        Card card = Resources.Load<Card>("Cards/CreatedCards/" + _cardName);
        GameObject cardTable = card.spawnCard();
        if (_isPlayers)
        {
            cardTable.transform.SetParent(playerBoard.transform);
            cardTable.transform.localScale = Vector3.one;
            cardTable.GetComponent<Attack>().enabled = true;
            cardTable.GetComponent<CardStatsHelper>().enabled = true;
            RemoveCardFromHand(_cardName);
        }
        else
        {
            cardTable.transform.SetParent(enemyBoard.transform);
            cardTable.GetComponent<Attack>().enabled = true;
            cardTable.GetComponent<CardStatsHelper>().enabled = true;
            cardTable.transform.localScale = Vector3.one;
        }
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

    public void RemoveCardFromHand(string cardName)
    {
        foreach (Transform child in handCanvas.transform)
        {
            if (child.name == cardName)
            {
                Destroy(child.gameObject);
                break;
            }
        }
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
