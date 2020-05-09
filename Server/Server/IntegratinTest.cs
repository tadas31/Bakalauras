using Xunit;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;

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
                    { (int)ServerPackets.putCardOnTable, PutCardOnTable}
        //pullStartingCards,
        //setTurn,
        //setTimer,
        //putCardOnTable,
        //setLife,
        //setMana,
        //setMaxMana,
        //attack,
        //setEnemyCardCount,
        //pulledCard,
        //attackPlayer,
        //deckCount
                };
            }

            private void Disconnect()
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
                welcomeReceived = true;
            }
        }

        public static int id;

        public static void SpawnPlayer(Packet _packet)
        {
            received = false;
            id = _packet.ReadInt();
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

        static bool serverStarted = false;
        void StartServerAndConnect() 
        {
            if (!serverStarted)
            {
                serverStarted = true;
                Program.Main(new string[1]);
                Client client = new Client();
                client.Awake();
                Client.instance.Start();
                Client.instance.ConnectToServer();
            }
        }
        [Fact, TestPriority(-2)]
        public void PlaceCardToTableClient()
        {
            StartServerAndConnect();
            received = false;
            Server.clients[id].player.mana = 100;
            using (Packet _packet = new Packet((int)ClientPackets.placeCardToTable))
            {
                _packet.Write("goblin");

                SendTCPData(_packet);
            }
            System.Threading.Thread.Sleep(500);
            Assert.True(received);
        }
        
        [Fact, TestPriority(0)]
        public void WelcomeReceivedClient()
        {
            StartServerAndConnect();
            received = false;
            using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
            {
                _packet.Write(id);
                _packet.Write("Username");
                _packet.Write("goblin");

                SendTCPData( _packet);
            }
            System.Threading.Thread.Sleep(250);
            Assert.True(received);
        }

        [Fact, TestPriority(-1)]
        public void ConnectToServerClient()
        {
            StartServerAndConnect();
            System.Threading.Thread.Sleep(250);
            Assert.True(welcomeReceived);
        }
       
    }
}
