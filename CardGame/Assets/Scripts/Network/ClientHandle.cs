using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();
        UIManager.instance.showConnect = false;

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived(); 
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        _username = _username.Remove(_username.Length - 1);

        ClassicGameManager.instance.SpawnPlayer(_id,_username);
    }

    public static void PullStartingCards(Packet _packet)
    {
        string _deck = _packet.ReadString();
        Debug.Log($"Got the hand cards : {_deck}");

        ClassicGameManager.instance.PullStartingCards(_deck);
    }

    public static void SetTurn(Packet _packet)
    {
        bool _isTurn = _packet.ReadBool();
        Debug.Log($"Turn variable : {_isTurn}");

        ClassicGameManager.instance.SetTurn(_isTurn);
    }

    public static void SetTimer(Packet _packet)
    {
        float _timer = _packet.ReadFloat();
        Debug.Log($"Reseting the timer to : {_timer}");

        TimerManager.timeLeft = _timer;
    }

    public static void SetLife(Packet _packet)
    {
        int _toClient = _packet.ReadInt();
        int _life = _packet.ReadInt();
        Debug.Log($"Setting life to {_toClient} : {_life}");

        ClassicGameManager.instance.SetLife(_toClient, _life);
    }
    public static void SetMana(Packet _packet)
    {
        int _toClient = _packet.ReadInt();
        int _mana = _packet.ReadInt();
        Debug.Log($"Setting mana to {_toClient} : {_mana}");

        ClassicGameManager.instance.SetMana(_toClient, _mana);
    }

    public static void SetMaxMana(Packet _packet)
    {
        int _toClient = _packet.ReadInt();
        int _maxMana = _packet.ReadInt();
        Debug.Log($"Setting max mana to {_toClient} : {_maxMana}");

        ClassicGameManager.instance.SetMaxMana(_toClient, _maxMana);
    }

    public static void SetDeckCount(Packet _packet)
    {
        int _deckCount = _packet.ReadInt();
        Debug.Log($"Setting the card count left in the deck");

        ClassicGameManager.instance.SetDeckCardCount(_deckCount);
    }

    public static void PutCardOnTable(Packet _packet)
    {
        bool _isPlayers = _packet.ReadBool();
        string _cardName = _packet.ReadString();
        bool removeFromHand = _packet.ReadBool();

        Debug.Log($"Putting card on table : {_cardName}, {_isPlayers}");

        ClassicGameManager.instance.PutOnTable(_cardName,_isPlayers, removeFromHand);
    }

    public static void PulledCard(Packet _packet)
    {
        string _cardName = _packet.ReadString();

        Debug.Log($"Pulling card {_cardName}");

        ClassicGameManager.instance.AddCardToHand(_cardName);
    }

    public static void SetEnemyCardCount(Packet _packet)
    {
        int _cardCount = _packet.ReadInt();

        Debug.Log($"Setting enemy card count {_cardCount}");

        EnemyHand.instance.SetCardCount(_cardCount);
    }

    public static void Attack(Packet _packet)
    {
        int _clientFrom = _packet.ReadInt();
        string _from = _packet.ReadString();
        int _fromLife = _packet.ReadInt();
        string _to = _packet.ReadString();
        int _toLife = _packet.ReadInt();

        Debug.Log($"Attacking from client {_clientFrom} {_from} to {_to}");

        ClassicGameManager.instance.Attack(_clientFrom, _from, _fromLife, _to, _toLife);
    }
    
    /// <summary>
    /// Reads attack player package and calls the appropriate method.
    /// </summary>
    public static void AttackPlayer(Packet _packet)
    {

        int _clientFrom = _packet.ReadInt();
        int _clientTo = _packet.ReadInt();
        string _from = _packet.ReadString();
        int _toLife = _packet.ReadInt();

        Debug.Log($"Attacking from client {_clientFrom} {_from} to {_clientTo}");

        ClassicGameManager.instance.AttackPlayer(_clientFrom, _clientFrom, _from, _toLife);
    }
}
