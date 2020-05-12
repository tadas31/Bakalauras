using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int PlayerCount 
        { 
            get {return _playerCount; } 
            set 
            {
                _playerCount = value;
                if (MaxPlayers == _playerCount)
                {
                    GameLogic.StartGame();
                }
            }
        }
        private static int _playerCount;
        public static int Port { get; private set; }
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
        public delegate void PacketHandler(int _fromClient, Packet _packet);
        public static Dictionary<int, PacketHandler> packetHandlers;

        private static TcpListener tcpListener;

        public static void Start(int _maxPlayers, int _port)
        {
            MaxPlayers = _maxPlayers;
            PlayerCount = 0;
            Port = _port;

            Console.WriteLine("Starting server...");
            InitializeServerData();

            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);

            Console.WriteLine($"Server started on port {Port}.");
        }

        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
            Console.WriteLine($"Incoming connection from {_client.Client.RemoteEndPoint}...");

            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(_client);
                    return;
                }
            }

            Console.WriteLine($"{_client.Client.RemoteEndPoint} failed to connect: Server full!");
        }

        private static void InitializeServerData()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                clients.Add(i, new Client(i));
            }

            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
                { (int)ClientPackets.endTurn, ServerHandle.EndTurn },
                { (int)ClientPackets.placeCardToTable, ServerHandle.PlaceCardToTable },
                { (int)ClientPackets.attack, ServerHandle.Attack }
            };
            Console.WriteLine("Initialized packets.");
        }
    }
}
