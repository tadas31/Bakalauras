using Xunit;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Diagnostics;

namespace Server
{
    public class IntegratinTest
    {
       
        public class Client
        {
            public static Client instance;
            public static int dataBufferSize = 4096;

            public string ip = "127.0.0.1";
            public int port = 26950;
            public int myId = 0;
            public TCP tcp;

            private bool isConnected = false;
            private delegate void PacketHandler(Packet _packet);
            private static Dictionary<int, PacketHandler> packetHandlers;

            public void Awake()
            {
                if (instance == null)
                {
                    instance = this;
                }
                else if (instance != this)
                {
                    //Remove the client
                }
            }

            public void Start()
            {
                tcp = new TCP();
            }

            private void OnApplicationQuit()
            {
                Disconnect();
            }

            public void ConnectToServer()
            {
                InitializeClientData();

                isConnected = true;
                tcp.Connect();
            }

            public class TCP
            {
                public TcpClient socket;

                private NetworkStream stream;
                private Packet receivedData;
                private byte[] receiveBuffer;

                public void Connect()
                {
                    socket = new TcpClient
                    {
                        ReceiveBufferSize = dataBufferSize,
                        SendBufferSize = dataBufferSize
                    };

                    receiveBuffer = new byte[dataBufferSize];
                    socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);
                }

                private void ConnectCallback(IAsyncResult _result)
                {
                    socket.EndConnect(_result);

                    if (!socket.Connected)
                    {
                        return;
                    }

                    stream = socket.GetStream();

                    receivedData = new Packet();

                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
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
                        //Error
                    }
                }

                private void ReceiveCallback(IAsyncResult _result)
                {
                    try
                    {
                        int _byteLength = stream.EndRead(_result);
                        if (_byteLength <= 0)
                        {
                            instance.Disconnect();
                            return;
                        }

                        byte[] _data = new byte[_byteLength];
                        Array.Copy(receiveBuffer, _data, _byteLength);

                        receivedData.Reset(HandleData(_data));
                        stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                    }
                    catch
                    {
                        Disconnect();
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
                                packetHandlers[_packetId](_packet);
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

                private void Disconnect()
                {
                    instance.Disconnect();

                    stream = null;
                    receivedData = null;
                    receiveBuffer = null;
                    socket = null;
                }
            }

            private void InitializeClientData()
            {
                packetHandlers = new Dictionary<int, PacketHandler>()
                {
                    { (int)ServerPackets.welcome,  Welcome},
                    { (int)ServerPackets.spawnPlayer, SpawnPlayer},
                    { (int)ServerPackets.putCardOnTable, PutCardOnTable},
                    { (int)ServerPackets.pullStartingCards, Empty},
                    { (int)ServerPackets.setTurn, Empty},
                    { (int)ServerPackets.setTimer, Empty},
                    { (int)ServerPackets.setLife, Empty},
                    { (int)ServerPackets.setMana, Empty},
                    { (int)ServerPackets.setMaxMana, SetMaxMana},
                    { (int)ServerPackets.attack, Attack},
                    { (int)ServerPackets.setEnemyCardCount, Empty},
                    { (int)ServerPackets.pulledCard, Empty},
                    { (int)ServerPackets.attackPlayer, Empty},
                    { (int)ServerPackets.deckCount, Empty},
                };
            }

            public void Disconnect()
            {
                if (isConnected)
                {
                    isConnected = false;
                    tcp.socket.Close();
                }
            }
        }
        private static void SendTCPData( Packet _packet)
        {
            _packet.WriteLength();
            Client.instance.tcp.SendData(_packet);
        }

        public static bool received = false;
        public static bool welcomeReceived = false;
        public static void Welcome(Packet _packet)
        {
            string _msg = _packet.ReadString();
            int _id = _packet.ReadInt();
            if (_msg != null && _id != 0)
            {
                Client.instance.myId = _id;
                welcomeReceived = true;
                WelcomeReceivedSend();
            }
           
        }

        public static void SpawnPlayer(Packet _packet)
        {
            received = false;
            Client.instance.myId = _packet.ReadInt();
            string username = _packet.ReadString();
            if (username == "Username")
            {
                received = true;
            }
        }

        public static void PutCardOnTable(Packet _packet)
        {
            received = false;
            bool players = _packet.ReadBool();
            string _cardName = _packet.ReadString();
            if (_cardName.ToLower() == "goblin")
            {
                received = true;
            }
        }

        public static void Attack(Packet _packet)
        {
            received = false;
            int id = _packet.ReadInt();
            string _cardName = _packet.ReadString();
            int lifeFrom = _packet.ReadInt();
            string _cardName1 = _packet.ReadString();
            int lifeTo = _packet.ReadInt();

            if (_cardName.ToLower() == "goblin" && _cardName1.ToLower() == "goblin")
            {
                received = true;
            }
        }

        static bool setMana = false;

        public static void SetMaxMana(Packet _packet)
        {
            received = false;
            int id = _packet.ReadInt();
            int maxMana = _packet.ReadInt();

            if (maxMana > 1)
            {
                setMana = true;
            }
        }

        public static void Empty(Packet packet)
        {

        }

        void StartServerAndConnect() 
        {
            Program.Main(new string[1]);
            Client client = new Client();
            client.Awake();
            Client.instance.Start();
            Client.instance.ConnectToServer();
        }
        
        static void WelcomeReceivedSend()
        {
            using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write("Username");
                _packet.Write("goblin");

                SendTCPData(_packet);
            }
        }

        [Fact]
        public void WelcomeReceivedClient()
        {
            StartServerAndConnect();
            received = false;
            using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write("Username");
                _packet.Write("goblin");

                SendTCPData( _packet);
            }
            System.Threading.Thread.Sleep(250);
            Client.instance.Disconnect();
            Program.Stop();
            Assert.True(received);
        }

        [Fact]
        public void ConnectToServerClient()
        {
            StartServerAndConnect();
            System.Threading.Thread.Sleep(250);
            Client.instance.Disconnect();
            Program.Stop();
            Assert.True(welcomeReceived);
        }

        [Fact]
        public void PlaceCardToTableClient()
        {
            StartServerAndConnect();
            System.Threading.Thread.Sleep(500);
            Server.clients[Client.instance.myId].player = new Player(1, "Username", "goblin");
            Server.clients[Client.instance.myId].player.mana = 100;
            Server.clients[Client.instance.myId].player.isTurn = true;
            Server.clients[Client.instance.myId].player.hand = new CardContainer("goblin");
            using (Packet _packet = new Packet((int)ClientPackets.placeCardToTable))
            {
                _packet.Write("goblin");

                SendTCPData(_packet);
            }
            System.Threading.Thread.Sleep(1000);
            Client.instance.Disconnect();
            Program.Stop();
            Assert.True(received);
        }

        [Fact]
        public void AttackClient()
        {
            StartServerAndConnect();
            System.Threading.Thread.Sleep(1000);
            Server.clients[Client.instance.myId].player = new Player(1, "Username", "goblin");
            Server.clients[Client.instance.myId].player.mana = 100;
            Server.clients[Client.instance.myId].player.isTurn = true;
            Server.clients[Client.instance.myId].player.table = new CardContainer("goblin");
            Server.clients[Client.instance.myId].player.table.SetCardsCanAttack(true);
            Server.clients[Client.instance.myId].enemyClient = Server.clients[2];
            Server.clients[Client.instance.myId].enemyClient.player = new Player(2, "Username", "goblin");
            Server.clients[Client.instance.myId].enemyClient.player.table = new CardContainer("goblin");
            using (Packet _packet = new Packet((int)ClientPackets.attack))
            {
                _packet.Write("goblin");
                _packet.Write("goblin");
                SendTCPData(_packet);
            }
            received = false;
            System.Threading.Thread.Sleep(1000);
            Client.instance.Disconnect();
            Program.Stop();
            Assert.True(received);
        }


        [Fact]
        public void EndTurnClient()
        {
            StartServerAndConnect();
            System.Threading.Thread.Sleep(1000);
            GameLogic.StartTimer();
            Server.clients[Client.instance.myId].player = new Player(1, "Username", "goblin");
            Server.clients[Client.instance.myId].player.mana = 100;
            Server.clients[Client.instance.myId].player.isTurn = true;
            Server.clients[Client.instance.myId].player.table = new CardContainer("goblin");
            Server.clients[Client.instance.myId].player.table.SetCardsCanAttack(true);
            Server.clients[Client.instance.myId].enemyClient = Server.clients[2];
            Server.clients[Client.instance.myId].enemyClient.player = new Player(2, "Username", "goblin");
            Server.clients[Client.instance.myId].enemyClient.player.table = new CardContainer("goblin");
            received = false;
            using (Packet _packet = new Packet((int)ClientPackets.endTurn))
            {
                SendTCPData(_packet);
            }
            System.Threading.Thread.Sleep(1000);
            Client.instance.Disconnect();
            Program.Stop();
            Assert.True(setMana);
        }
    }
}
