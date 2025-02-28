﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();
            string _deck = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            Server.clients[_fromClient].SendIntoGame(_username, _deck);
            Server.PlayerCount++;
        }

        public static void PlaceCardToTable(int _fromClient, Packet _packet)
        {
            string _cardName = _packet.ReadString();

            Server.clients[_fromClient].PutCardOnTable(_cardName);
        }

        public static void Attack(int _fromClient, Packet _packet)
        {
            string _attackFrom = _packet.ReadString();
            string _attackTo = _packet.ReadString();

            Server.clients[_fromClient].Attack(_attackFrom,_attackTo);
        }

        public static void EndTurn(int _fromClient, Packet _packet)
        {
            if (Server.clients[_fromClient].player.isTurn)
            {
                GameLogic.EndTurn();
            }
        }
    }
}
