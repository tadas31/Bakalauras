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

        public void PutCardOnTable(string _cardName)
        {
            if (player.HasInHand(_cardName));
            {
                Console.WriteLine($"Putting a {_cardName} card to player's {id}");
                player.PutToTable(_cardName);
                ServerSend.PutCardOnTable(id, true, _cardName);
                //Putting the card in the enemy side.
                foreach (Client _client in Server.clients.Values)
                {
                    if (_client.player != null)
                    {
                        if (_client.id != id)
                        {
                            ServerSend.PutCardOnTable(_client.id, false, _cardName);
                        }
                    }
                }
            }
        }


        public void Attack(string _attackFrom, string _attackTo)
        {
            //Check if it is your turn
            if (!player.isTurn)
            {
                return;
            }

            Client enemyClient = null;

            foreach (Client item in Server.clients.Values)
            {
                if (item.player != null)
                {
                    if (item.id != id)
                    {
                        enemyClient = item;
                    }
                }
            }

            if (enemyClient == null)
            {
                //Something wrong. Maybe there is no need for this part of the code.
                return;
            }

            //Get the card that with the card that you are attacking
            if (player.table.isInDeck(_attackFrom))
            {
                Card _attackFromCard = player.table.GetCard(_attackFrom);
                int retuneAttack = enemyClient.DealDamageTo(_attackFromCard.attack, _attackTo);
                //DealDamageTo.

                //Think about this part.
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

        public int DealDamageTo(int _amount, string _to) 
        {
            if (player.table.isInDeck(_to))
            {
                Card minion = player.table.GetCard(_to);
                minion.life -= _amount;
                return minion.attack;
                //TODO: Maybe need to send to all of the cards that it died.
            }
            return 0;
        }

        public void Disconnect()
        {
            Console.WriteLine($"{tcp.socket.Client.RemoteEndPoint} had disconnected.");

            player = null;
            tcp.Disconnect();
        }
    }
}
