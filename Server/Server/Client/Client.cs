using System;
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
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
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

        public void SendPulledCard()
        {
            ServerSend.PulledCard(id, player.PullCardToHand().cardName);
        }

        public void PutCardOnTable(string _cardName)
        {
            if (player.HasInHand(_cardName) && player.HasEnoughMana(_cardName) && player.isTurn)
            {
                Console.WriteLine($"Putting a {_cardName} card to player's {id}");
                player.PutOnTable(_cardName);
                ServerSend.PutCardOnTable(id, true, _cardName);
                ServerSend.SetMana(id, player.mana);
                //Putting the card in the enemy side.
                foreach (Client _client in Server.clients.Values)
                {
                    if (_client.player != null)
                    {
                        if (_client.id != id)
                        {
                            ServerSend.PutCardOnTable(_client.id, false, _cardName);
                            ServerSend.SetEnemyCardCount(_client.id, player.CardCountInHand());
                        }
                    }
                }
            }
            //TODO: Can be that it is returned to the client that something is not okey. ???
        }


        public void Attack(string _attackFrom, string _attackTo)
        {
            //Check if it is your turn
            if (!player.isTurn)
            {
                return;
            }

            if (enemyClient == null)
            {
                //Something wrong. Maybe there is no need for this part of the code.
                return;
            }

            //Get the card that with the card that you are attacking
            if (player.table.IsInDeck(_attackFrom))
            {
                CardAttack(_attackFrom, _attackTo);
            }
            else if (false) //If it is attacking with a spell card
            {

            }
            else if (false) //If the player it self is attacking
            {

            }
            else
            {
                return;
            }
            //Get the card that the other player has.
            //Deal damage to the card
            //Send the information back to the clients

            //TODO: If the player is attacked
            //TODO: If the another minion is attacked

        }

        /// <summary>
        /// Card attack to an object
        /// </summary>
        /// <param name="_attackFrom">Attack cards name</param>
        /// <param name="_attackTo">The object that is attacked from</param>
        public void CardAttack( string _attackFrom, string _attackTo) 
        { 
           
            int n = -1;
            bool isNumeric = int.TryParse(_attackTo, out n);
            Card _attackFromCard = player.table.GetCard(_attackFrom);
            //If the card is attacking players health points
            if (isNumeric && enemyClient.id == n)
            {
                enemyClient.player.life -= _attackFromCard.attack;
                ServerSend.AttackPlayer(id, enemyClient.id, _attackFrom, enemyClient.player.life);
            }
            else
            {
                Card _attackToCard = enemyClient.DealDamageTo(_attackFromCard.attack, _attackTo);
                DealDamageTo(_attackToCard.attack, _attackFrom);
                ServerSend.Attack(id, _attackFrom, _attackFromCard.life, _attackTo, _attackToCard.life);
            }
        }

        public Card DealDamageTo(int _amount, string _to)
        {
            if (player.table.IsInDeck(_to))
            {
                Card minion = player.table.GetCard(_to);
                minion.life -= _amount;
                return minion;
                //TODO: Maybe need to send to all of the cards that it died.
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

            player = null;
            tcp.Disconnect();
        }
    }
}
