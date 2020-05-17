using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Server
{
    class GameLogic
    {
        public static Timer timer;

        public static void Update()
        {
            ThreadManager.UpdateMain();
        }

        /// <summary>
        /// Calls all of the needed methods to start the game.
        /// </summary>
        public static void StartGame()
        {
            StartSetTurns();
            SendTurnsForPlayers();
            PullStartingCardsForPlayers();
            StartTimer();
            SendLifeToPlayers();
            SendManaToPlayers();
            SendMaxManaToPlayers();
            SetEnemyCardCountToPlayers();
        }

        public static void StartTimer()
        {
            timer = new Timer(Constants.TURN_TIME_MILISECONDS);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
            SendTimerInfoForPlayers(Constants.TURN_TIME_SECONDS);
        }

        public static void StartSetTurns()
        {
            Random random = new Random();
            int randInt = random.Next(1, Server.MaxPlayers + 1);
            Server.clients[randInt].player.isTurn = true;
            Console.WriteLine($"The starting turn is of {randInt}");
        }

        public static void ChangeTurns()
        {
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    _client.player.isTurn = !_client.player.isTurn;
                    _client.TableCardsCanAttack(_client.player.isTurn);
                }
            }
        }

        public static void SendLifeToPlayers()
        {
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    Console.WriteLine($"Sending life information to {_client.id}");
                    _client.SendLife();
                }
            }
        }

        public static void SendManaToPlayers()
        {
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    Console.WriteLine($"Sending mana information to {_client.id}");
                    _client.SendMana();
                }
            }
        }

        public static void SendMaxManaToPlayers()
        {
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    Console.WriteLine($"Sending max mana information to {_client.id}");
                    _client.SendMaxMana();
                }
            }
        }

        public static void SendTurnsForPlayers()
        {
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    Console.WriteLine($"Setting the turn for {_client.id}");
                    _client.SendTurn();
                }
            }
        }

        public static void SendTimerInfoForPlayers(float _time)
        {
            Console.WriteLine($"Setting the time for the turn to be {_time}");
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    _client.SendTimer(_time);
                }
            }
        }

        public static void PullStartingCardsForPlayers()
        {
            Console.WriteLine("Sending the starting cards to all of the players.");
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    Console.WriteLine($"Sending message to add cards to hand for {_client.id}.");
                    ServerSend.PullStartingCards(_client.id, _client.player.PullStartingCards());
                    _client.SendDeckCount();
                }
            }
        }

        public static void SetEnemyCardCountToPlayers()
        {
            Console.WriteLine("Sending the starting cards to all of the players.");
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    _client.SetEnemyCardCount();
                }
            }
        }

        public static void AddManaToCurrentTurnPlayer()
        {
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    if (_client.player.isTurn)
                    {
                        Console.WriteLine($"Adding to the {_client.id} player mana.");
                        _client.player.AddMaxMana();
                        _client.player.ResetMana();
                        return;
                    }

                }
            }
        }

        public static void SendPulledCardToCurrentTurn()
        {
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    if (_client.player.isTurn)
                    {
                        _client.SendPulledCard();
                        _client.SetEnemyCardCount();
                        return;
                    }

                }
            }
        }

        public static void EndTurn()
        {
            AddManaToCurrentTurnPlayer();
            SendManaToPlayers();
            SendMaxManaToPlayers();
            ChangeTurns();
            SendTurnsForPlayers();
            SendPulledCardToCurrentTurn();
            SendTimerInfoForPlayers(Constants.TURN_TIME_SECONDS);
            timer.Stop();
            timer.Start();
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
            EndTurn();
        }
    }
}
