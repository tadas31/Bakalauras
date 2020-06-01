using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        #region packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SpawnPlayer(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void PullStartingCards(int _toClient, string _cards)
        {
            using (Packet _packet = new Packet((int)ServerPackets.pullStartingCards))
            {
                _packet.Write(_cards);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SetTurn(int _toClient, bool _isTurn)
        {
            using (Packet _packet = new Packet((int)ServerPackets.setTurn))
            {
                _packet.Write(_isTurn);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SetTimer(int _toClient, float _time)
        {
            using (Packet _packet = new Packet((int)ServerPackets.setTimer))
            {
                _packet.Write(_time);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SetLife(int _toClient, int _life)
        {
            using (Packet _packet = new Packet((int)ServerPackets.setLife))
            {
                _packet.Write(_toClient);
                _packet.Write(_life);

                SendTCPDataToAll(_packet);
            }
        }

        public static void SetMana(int _toClient, int _mana)
        {
            using (Packet _packet = new Packet((int)ServerPackets.setMana))
            {
                _packet.Write(_toClient);
                _packet.Write(_mana);

                SendTCPDataToAll(_packet);
            }
        }

        public static void SetMaxMana(int _toClient, int _maxMana)
        {
            using (Packet _packet = new Packet((int)ServerPackets.setMaxMana))
            {
                _packet.Write(_toClient);
                _packet.Write(_maxMana);

                SendTCPDataToAll(_packet);
            }
        }

        /// <summary>
        /// Set the remaining cards in the player card.
        /// </summary>
        /// <param name="_toClient">The client's id</param>
        /// <param name="count">The amount of cards</param>
        public static void SetDeckCount(int _toClient, int _count)
        {
            using (Packet _packet = new Packet((int)ServerPackets.deckCount))
            {
                _packet.Write(_count);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void PulledCard(int _toClient, string _cardName)
        {
            using (Packet _packet = new Packet((int)ServerPackets.pulledCard))
            {
                _packet.Write(_cardName);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void Attack(int _clientId, string _from, int _lifeFrom, string _to, int _lifeTo)
        {
            using (Packet _packet = new Packet((int)ServerPackets.attack))
            {
                _packet.Write(_clientId);
                _packet.Write(_from);
                _packet.Write(_lifeFrom);
                _packet.Write(_to);
                _packet.Write(_lifeTo);

                Console.WriteLine($"Sending attack information form {_clientId} to all clients to attack from {_from} to {_to}");

                SendTCPDataToAll(_packet);
            }
        }

        public static void AttackPlayer(int _clientId,int _toClient, string _from,  int _lifeTo)
        {
            using (Packet _packet = new Packet((int)ServerPackets.attackPlayer))
            {
                _packet.Write(_clientId);
                _packet.Write(_toClient);
                _packet.Write(_from);
                _packet.Write(_lifeTo);

                Console.WriteLine($"Sending attack information form {_clientId} to all clients to attack player from {_from}");

                SendTCPDataToAll(_packet);
            }
        }

        public static void PutCardOnTable(int _toClient, bool _isPlayers, string _cardName, bool _removeFromHand = true)
        {
            using (Packet _packet = new Packet((int)ServerPackets.putCardOnTable))
            {
                _packet.Write(_isPlayers);
                _packet.Write(_cardName);
                _packet.Write(_removeFromHand);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SetEnemyCardCount(int _toClient, int _count)
        {
            using (Packet _packet = new Packet((int)ServerPackets.setEnemyCardCount))
            {
                _packet.Write(_count);

                SendTCPData(_toClient, _packet);
            }
        }
        #endregion
    }
}
