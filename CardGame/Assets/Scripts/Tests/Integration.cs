using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Integration
    {
        [Test]
        public void SpawnPlayerPlayerServer()
        {
            GameObject managerObject = GameObject.Find("GameManager");
            ClassicGameManager manager = managerObject.GetComponent<ClassicGameManager>();
            manager.Awake();
            Client client = GameObject.Find("ClientManager").GetComponent<Client>();
            client.Awake();
            client.myId = 1;
            using (Packet _packet = new Packet())
            {
                _packet.Write(1);
                _packet.Write("User?");
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.SpawnPlayer(packet);
            }
            Assert.AreEqual("User", ClassicGameManager.instance.localPlayer.GetComponent<PlayerManager>().username);
            ClassicGameManager.players = new Dictionary<int, PlayerManager>();
        }

        [Test]
        public void SpawnPlayerEnemyServer()
        {
            GameObject managerObject = GameObject.Find("GameManager");
            ClassicGameManager manager = managerObject.GetComponent<ClassicGameManager>();
            manager.Awake();
            Client client = GameObject.Find("ClientManager").GetComponent<Client>();
            client.Awake();
            client.myId = 1;
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            using (Packet _packet = new Packet())
            {
                _packet.Write(2);
                _packet.Write("User?");
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.SpawnPlayer(packet);
            }
            Assert.AreEqual("User", ClassicGameManager.instance.enemyPlayer.GetComponent<PlayerManager>().username);
            ClassicGameManager.players = new Dictionary<int, PlayerManager>();
        }

        [Test]
        public void PullStartingCardsServer()
        {
            GameObject managerObject = GameObject.Find("GameManager");
            ClassicGameManager manager = managerObject.GetComponent<ClassicGameManager>();
            manager.Awake();
            Client client = GameObject.Find("ClientManager").GetComponent<Client>();
            client.Awake();
            client.myId = 1;
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            using (Packet _packet = new Packet())
            {
                _packet.Write("goblin");
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.PullStartingCards(packet);
            }
            Assert.AreEqual(1, ClassicGameManager.instance.handCanvas.transform.childCount);
            ClassicGameManager.players = new Dictionary<int, PlayerManager>();
        }

        [Test]
        public void SetTurnServer()
        {
            GameObject managerObject = GameObject.Find("GameManager");
            ClassicGameManager manager = managerObject.GetComponent<ClassicGameManager>();
            manager.Awake();
            Client client = GameObject.Find("ClientManager").GetComponent<Client>();
            client.Awake();
            client.myId = 1;
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            ClassicGameManager.curPlayer.isTurn = false;
            using (Packet _packet = new Packet())
            {
                _packet.Write(1);
                _packet.Write("User?");
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.SpawnPlayer(packet);
            }
            using (Packet _packet = new Packet())
            {
                _packet.Write(true);
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.SetTurn(packet);
            }
            Assert.True(ClassicGameManager.curPlayer.isTurn);
            ClassicGameManager.players = new Dictionary<int, PlayerManager>();
        }

        [Test]
        public void SetTimerServer()
        {
            GameObject managerObject = GameObject.Find("GameManager");
            ClassicGameManager manager = managerObject.GetComponent<ClassicGameManager>();
            manager.Awake();
            Client client = GameObject.Find("ClientManager").GetComponent<Client>();
            client.Awake();
            client.myId = 1;
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();

            using (Packet _packet = new Packet())
            {
                _packet.Write(60.0f);
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.SetTimer(packet);
            }
            Assert.AreEqual(60, TimerManager.timeLeft);
            ClassicGameManager.players = new Dictionary<int, PlayerManager>();
        }

        [Test]
        public void SetLifeServer()
        {
            GameObject managerObject = GameObject.Find("GameManager");
            ClassicGameManager manager = managerObject.GetComponent<ClassicGameManager>();
            manager.Awake();
            Client client = GameObject.Find("ClientManager").GetComponent<Client>();
            client.Awake();
            client.myId = 1;
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            ClassicGameManager.curPlayer.isTurn = false;
            using (Packet _packet = new Packet())
            {
                _packet.Write(1);
                _packet.Write("User?");
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.SpawnPlayer(packet);
            }
            using (Packet _packet = new Packet())
            {
                _packet.Write(1);
                _packet.Write(50);
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.SetLife(packet);
            }
            Assert.AreEqual(50, ClassicGameManager.curPlayer.life);
            ClassicGameManager.players = new Dictionary<int, PlayerManager>();
        }

        [Test]
        public void SetManaServer()
        {
            GameObject managerObject = GameObject.Find("GameManager");
            ClassicGameManager manager = managerObject.GetComponent<ClassicGameManager>();
            manager.Awake();
            Client client = GameObject.Find("ClientManager").GetComponent<Client>();
            client.Awake();
            client.myId = 1;
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            ClassicGameManager.curPlayer.isTurn = false;
            using (Packet _packet = new Packet())
            {
                _packet.Write(1);
                _packet.Write("User?");
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.SpawnPlayer(packet);
            }
            using (Packet _packet = new Packet())
            {
                _packet.Write(1);
                _packet.Write(50);
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.SetMana(packet);
            }
            Assert.AreEqual(50, ClassicGameManager.curPlayer.mana);
            ClassicGameManager.players = new Dictionary<int, PlayerManager>();
        }

        [Test]
        public void SetMaxServer()
        {
            GameObject managerObject = GameObject.Find("GameManager");
            ClassicGameManager manager = managerObject.GetComponent<ClassicGameManager>();
            manager.Awake();
            Client client = GameObject.Find("ClientManager").GetComponent<Client>();
            client.Awake();
            client.myId = 1;
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            ClassicGameManager.curPlayer.isTurn = false;
            using (Packet _packet = new Packet())
            {
                _packet.Write(1);
                _packet.Write("User?");
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.SpawnPlayer(packet);
            }
            using (Packet _packet = new Packet())
            {
                _packet.Write(1);
                _packet.Write(50);
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.SetMaxMana(packet);
            }
            Assert.AreEqual(50, ClassicGameManager.curPlayer.maxMana);
            ClassicGameManager.players = new Dictionary<int, PlayerManager>();
        }

        [Test]
        public void PutCardOnTableServer()
        {
            GameObject managerObject = GameObject.Find("GameManager");
            ClassicGameManager manager = managerObject.GetComponent<ClassicGameManager>();
            manager.Awake();
            Client client = GameObject.Find("ClientManager").GetComponent<Client>();
            client.Awake();
            Client.instance.myId = 2;
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            ClassicGameManager.players = new Dictionary<int, PlayerManager>();
            
            foreach (Transform child in ClassicGameManager.instance.playerBoard.transform)
            {
                child.SetParent(null);
            }
            ClassicGameManager.instance.SpawnPlayer(2, "username");
        
            using (Packet _packet = new Packet())
            {
                _packet.Write(true);
                _packet.Write("rat");
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.PutCardOnTable(packet);
            }
            Assert.AreEqual("Rat", ClassicGameManager.instance.playerBoard.transform.GetChild(0).name);
            ClassicGameManager.players = new Dictionary<int, PlayerManager>();
        }

        [Test]
        public void PulledCardServer()
        {
            ClassicGameManager manager = GameObject.Find("GameManager").GetComponent<ClassicGameManager>();
            manager.Awake();
         
            using (Packet _packet = new Packet())
            {
                _packet.Write("Goblin");
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.PulledCard(packet);
            }
            Assert.AreEqual("Goblin", ClassicGameManager.instance.handCanvas.transform.GetChild(0).name);
            ClassicGameManager.players = new Dictionary<int, PlayerManager>();
            ClassicGameManager.instance.handCanvas.transform.GetChild(0).SetParent(null);
        }

        [Test]
        public void SetEnemyCardCountServer()
        {
            GameObject.Find("GameManager").GetComponent<ClassicGameManager>().enemyPlayer.SetActive(true);

            EnemyHand enemyHand = GameObject.Find("/Canvas/Enemy/CardsInHand").GetComponent<EnemyHand>();
            enemyHand.Awake();

            

            using (Packet _packet = new Packet())
            {
                _packet.Write(8);
                _packet.Length();
                Packet packet = new Packet(_packet.ToArray());

                ClientHandle.SetEnemyCardCount(packet);
            }
            Assert.AreEqual(8, EnemyHand.instance.cardCount);

        }
    }
}
