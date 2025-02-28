﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Client
    {
        public static int dataBufferSize = 4096;

        public int id;
        public Player player;
        public TCP tcp;
        public Client enemyClient;

        public Client(int _clientId)
        {
            id = _clientId;
            tcp = new TCP(id);
        }

        public class TCP
        {
            public TcpClient socket;

            private readonly int id;
            private NetworkStream stream;
            private Packet receivedData;
            private byte[] receiveBuffer;

            public TCP(int _id)
            {
                id = _id;
            }

            public void Connect(TcpClient _socket)
            {
                socket = _socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                receivedData = new Packet();
                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

                ServerSend.Welcome(id, "Welcome to the server!");
            }

            public void SendData(Packet _packet)
            {
                try
                {
                    if (socket != null)
                    {
                        stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                    }
                }
                catch (Exception _ex)
                {
                    Console.WriteLine($"Error sending data to player {id} via TCP: {_ex}");
                }
            }

            private void ReceiveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = stream.EndRead(_result);
                    if (_byteLength <= 0)
                    {
                        Server.clients[id].Disconnect();
                        Server.clients[id] = new Client(id);
                        return;
                    }

                    byte[] _data = new byte[_byteLength];
                    Array.Copy(receiveBuffer, _data, _byteLength);

                    receivedData.Reset(HandleData(_data));
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                }
                catch (Exception _ex)
                {
                    Console.WriteLine($"Error receiving TCP data: {_ex}");
                    Server.clients[id].Disconnect();
                }
            }

            private bool HandleData(byte[] _data)
            {
                int _packetLength = 0;

                receivedData.SetBytes(_data);

                if (receivedData.UnreadLength() >= 4)
                {
                    _packetLength = receivedData.ReadInt();
                    if (_packetLength <= 0)
                    {
                        return true;
                    }
                }

                while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength())
                {
                    byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
                    ThreadManager.ExecuteOnMainThread(() =>
                    {
                        using (Packet _packet = new Packet(_packetBytes))
                        {
                            int _packetId = _packet.ReadInt();
                            Server.packetHandlers[_packetId](id, _packet);
                        }
                    });

                    _packetLength = 0;
                    if (receivedData.UnreadLength() >= 4)
                    {
                        _packetLength = receivedData.ReadInt();
                        if (_packetLength <= 0)
                        {
                            return true;
                        }
                    }
                }

                if (_packetLength <= 1)
                {
                    return true;
                }

                return false;
            }

            public void Disconnect()
            {
                socket.Close();
                stream = null;
                receivedData = null;
                receiveBuffer = null;
                socket = null;
                Server.PlayerCount--;
            }
        }

        public void SendIntoGame(string _username, string _deck)
        {
            player = new Player(id, _username, _deck);
            //Send the player to all other players to be spawned.
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    if (_client.id != id)
                    {
                        enemyClient = _client;
                        ServerSend.SpawnPlayer(id, _client.player);
                    }
                }
            }
            //Spawn all of the players to newly connected player.
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    //Add enemy client if it is not this instance.
                    if(_client.id != id)
                        _client.enemyClient = this;
                    ServerSend.SpawnPlayer(_client.id, player);
                }
            }
        }

        public void SendTurn()
        {
            ServerSend.SetTurn(id, player.isTurn);
        }

        public void SendTimer(float _time)
        {
            ServerSend.SetTimer(id, _time);
        }

        public void SendLife()
        {
            ServerSend.SetLife(id, player.life);
        }

        public void SendMana()
        {
            ServerSend.SetMana(id, player.mana);
        }

        public void SendMaxMana()
        {
            ServerSend.SetMaxMana(id, player.maxMana);
        }

        public void SendDeckCount()
        {
            ServerSend.SetDeckCount(id, player.deck.CardCount());
        }

        public void SendPulledCard()
        {
            Card _card = player.PullCardToHand();
            if (_card != null)
            {
                ServerSend.PulledCard(id, _card.cardName);
                SendDeckCount();
            }
            
        }

        public void PutCardOnTable(string _cardName)
        {
            if (player.HasInHand(_cardName) && player.HasEnoughMana(_cardName) && player.isTurn)
            {
                Console.WriteLine($"Putting a {_cardName} card to player's {id}");
                
                //If the card is a spell.
                Card _putCard = player.hand.GetCard(_cardName);
                if (_putCard.type.ToLower().Contains("spell"))
                {
                    foreach (string item in _putCard.scripts)
                    {
                        if (item.ToLower().Contains("damageallcards"))
                        {
                            DamageAllEnemies(_putCard.attack);
                            DamageAllMyCards(_putCard.attack);
                        }
                        else if (item.ToLower().Contains("damageallenemies"))
                        {
                            DamageAllEnemies(_putCard.attack);
                        }
                    }
                }

                player.PutOnTable(_cardName);
                ServerSend.PutCardOnTable(id, true, _cardName);
                ServerSend.SetMana(id, player.mana);
                int enemeyId = -1;
                //Putting the card in the enemy side.
                foreach (Client _client in Server.clients.Values)
                {
                    if (_client.player != null)
                    {
                        if (_client.id != id)
                        {
                            enemeyId = _client.id;
                            ServerSend.PutCardOnTable(_client.id, false, _cardName);
                            ServerSend.SetEnemyCardCount(_client.id, player.CardCountInHand());
                        }
                    }
                }

                if (_putCard.HasScript("battlecrysummoncopy"))
                {
                    player.PutOnTable(_cardName, false);
                    ServerSend.PutCardOnTable(id, true, _cardName);
                    ServerSend.PutCardOnTable(enemeyId, false, _cardName);
                    ServerSend.SetEnemyCardCount(enemeyId, player.CardCountInHand());
                }
            }
        }

        public void TableCardsCanAttack(bool _canAttack)
        {
            player.TableCardsCanAttack(_canAttack);
        }

        public void DamageAllMyCards(int damage) 
        {
            player.DamagePlayersTableCards(damage);
        }

        public void DamageAllEnemies(int damage)
        {
            enemyClient.DamageAllMyCards(damage);
        }

        public void Attack(string _attackFrom, string _attackTo)
        {
            //Check if it is your turn
            if (!player.isTurn)
            {
                return;
            }

            //Get the card that with the card that you are attacking
            if (player.table.IsInDeck(_attackFrom))
            {
                CardAttack(_attackFrom, _attackTo);
            }
        }

        /// <summary>
        /// Card attack to an object
        /// </summary>
        /// <param name="_attackFrom">Attack cards name</param>
        /// <param name="_attackTo">The object that is attacked from</param>
        public void CardAttack( string _attackFrom, string _attackTo) 
        { 
           //Check is it id.
            int n = -1;
            bool isNumeric = int.TryParse(_attackTo, out n);
            //Get the card form which the player is attacking.
            Card _attackFromCard = player.table.GetCard(_attackFrom);

            //Can the card attack.
            if (!_attackFromCard.canAttack)
            {
                return;
            }

            //If the card is attacking players health points
            if (isNumeric && enemyClient.id == n)
            {
                if (!enemyClient.player.table.HasTauntCard())
                {
                    enemyClient.player.life -= _attackFromCard.attack;
                    ServerSend.AttackPlayer(id, enemyClient.id, _attackFrom, enemyClient.player.life);
                    _attackFromCard.canAttack = false;
                }
            }
            else
            {
                Card _attackToCard = enemyClient.DealDamageTo(_attackFromCard.attack, _attackTo);
                DealDamageTo(_attackToCard.attack, _attackFrom);
                ServerSend.Attack(id, _attackFrom, _attackFromCard.life, _attackTo, _attackToCard.life);
                _attackFromCard.canAttack = false;
            }
        }

        public Card DealDamageTo(int _amount, string _to)
        {
            if (player.table.IsInDeck(_to))
            {
                Card minion = player.table.GetCard(_to);
                minion.life -= _amount;
                if (minion.life <= 0)
                {
                    player.graveYard.AddToDeck(player.table.PullCard(minion.cardName));
                }
                return minion;
            }
            return null;
        }

        public void SetEnemyCardCount()
        {
            foreach (Client item in Server.clients.Values)
            {
                if (item.player != null)
                {
                    if (item.id != id)
                    {
                        ServerSend.SetEnemyCardCount(id, item.player.CardCountInHand());
                    }
                }
            }
        }

        public void Disconnect()
        {
            Console.WriteLine($"{tcp.socket.Client.RemoteEndPoint} had disconnected.");

            player.life = -1;
            SendLife();
            player = null;
            tcp.Disconnect();
        }
    }
}
