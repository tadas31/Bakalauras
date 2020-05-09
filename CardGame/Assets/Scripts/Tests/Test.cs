using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Test
    {

        //Attack helper tests
        [Test]
        public void GetAllPlayerCardsAttackHelper()
        {
            AttackHelper attackHelper = new GameObject().AddComponent<AttackHelper>();

            GameObject board = GameObject.Find("PlayerBoard");


            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            cardHand.transform.SetParent(board.transform);

            List<Transform> cards = attackHelper.getAllPlayerCards();

            Assert.AreEqual(1, cards.Count);
        }

        [Test]
        public void GetAllEnemyCardsAttackHelper()
        {
            AttackHelper attackHelper = new GameObject().AddComponent<AttackHelper>();

            GameObject board = GameObject.Find("EnemyBoard");


            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            cardHand.transform.SetParent(board.transform);

            List<Transform> cards = attackHelper.getAllEnemyCards();

            Assert.AreEqual(1, cards.Count);
            cardHand.transform.SetParent(null);
        }

        [Test]
        public void SetPlayersCardsToAttackAttackHelper()
        {
            AttackHelper attackHelper = new GameObject().AddComponent<AttackHelper>();

            GameObject board = GameObject.Find("EnemyBoard");

            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            cardHand.GetComponent<Attack>().attacked = true;
            cardHand.transform.SetParent(board.transform);

            attackHelper.SetPlayersCardsToAttack();


            Assert.True(cardHand.GetComponent<Attack>().attacked);
            cardHand.transform.SetParent(null);
        }

        [Test]
        public void SetPlayersCardsToNotAttackAttackHelper()
        {
            AttackHelper attackHelper = new GameObject().AddComponent<AttackHelper>();

            GameObject board = GameObject.Find("EnemyBoard");

            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            cardHand.GetComponent<Attack>().attacked = false;
            cardHand.transform.SetParent(board.transform);

            attackHelper.SetPlayersCardsToNotAttack();

            Assert.False(cardHand.GetComponent<Attack>().attacked);
        }


        [Test]
        public void getDeffendingPlayerAttackHelper()
        {
            AttackHelper attackHelper = new GameObject().AddComponent<AttackHelper>();

            GameObject board = GameObject.Find("EnemyBoard");

            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            cardHand.GetComponent<Attack>().attacked = false;
            cardHand.transform.SetParent(board.transform);

            attackHelper.SetPlayersCardsToNotAttack();

            Assert.False(cardHand.GetComponent<Attack>().attacked);
        }

        // Cost Helper tests


        [Test]
        public void GetCostCardCostHelper()
        {
            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            CardCostHelper helper = cardHand.GetComponent<CardCostHelper>();
            helper.Start();
            Assert.AreEqual(3, helper.getCost());
        }

        [Test]
        public void SetCostCostHelper()
        {
            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            CardCostHelper helper = cardHand.GetComponent<CardCostHelper>();
            helper.Start();
            helper.setCost(1);
            Assert.AreEqual(1, helper.getCost());
        }

        [Test]
        public void SetCostToLowCostHelper()
        {
            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            CardCostHelper helper = cardHand.GetComponent<CardCostHelper>();
            helper.Start();
            helper.setCost(-1);
            Assert.AreEqual(3, helper.getCost());
        }

        [Test]
        public void RemoveCostCostHelper()
        {
            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            CardCostHelper helper = cardHand.GetComponent<CardCostHelper>();
            helper.Start();
            helper.removeCost(5);
            Assert.AreEqual(3, helper.getCost());
        }

        [Test]
        public void RemoveCostToManyCostHelper()
        {
            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            CardCostHelper helper = cardHand.GetComponent<CardCostHelper>();
            helper.Start();
            helper.removeCost(7);
            Assert.AreEqual(3, helper.getCost());
        }

        //Card CardStatsHelper tests
        [Test]
        public void GetLifeCardStatsHelper()
        {
            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            CardStatsHelper helper = cardHand.GetComponent<CardStatsHelper>();
            helper.Start();
            Assert.AreEqual(2, helper.getLife());
        }

        [Test]
        public void TakeDamageCardStatsHelper()
        {
            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            CardStatsHelper helper = cardHand.GetComponent<CardStatsHelper>();
            helper.Start();
            helper.takeDamage(1);
            Assert.AreEqual(1, helper.getLife());
        }

        [Test]
        public void GetAttackCardStatsHelper()
        {
            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            CardStatsHelper helper = cardHand.GetComponent<CardStatsHelper>();
            helper.Start();
            Assert.AreEqual(3, helper.getAttack());
        }

        [Test]
        public void SetAttackCardStatsHelper()
        {
            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            CardStatsHelper helper = cardHand.GetComponent<CardStatsHelper>();
            helper.Start();
            helper.setAttack(1);
            Assert.AreEqual(1, helper.getAttack());
        }

        [Test]
        public void CheckIfStillAliveCardStatsHelper()
        {
            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            CardStatsHelper helper = cardHand.GetComponent<CardStatsHelper>();
            helper.Start();
            helper.takeDamage(1);
            helper.checkIfSitllAlive();
            Assert.IsNotNull(cardHand);
        }

        [Test]
        public void ResetStatsCardStatsHelper()
        {
            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            CardStatsHelper helper = cardHand.GetComponent<CardStatsHelper>();
            helper.Start();
            helper.takeDamage(1);
            helper.ResetStats();
            Assert.AreEqual(2, helper.getLife());
        }

        //ClasicGameManager test    
        [Test]
        public  void SpawnPlayerPlayerClassicGameManager()
        {
            GameObject managerObject = GameObject.Find("GameManager");
            ClassicGameManager manager = managerObject.GetComponent<ClassicGameManager>();
            manager.Awake();
            Client client = GameObject.Find("ClientManager").GetComponent<Client>();
            client.Awake();
            Client.instance.myId = 2;
            ClassicGameManager.players = new Dictionary<int, PlayerManager>();
            ClassicGameManager.instance.SpawnPlayer(2, "username");
            Assert.True(ClassicGameManager.instance.localPlayer.activeSelf);
        }

        //ClasicGameManager test    
        [Test]
        public  void SpawnPlayerEnemyClassicGameManager()
        {
            GameObject managerObject = GameObject.Find("GameManager");
            ClassicGameManager manager = managerObject.GetComponent<ClassicGameManager>();
            manager.Awake();
            Client client = GameObject.Find("ClientManager").GetComponent<Client>();
            client.Awake();
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            Client.instance.myId = 2;
            ClassicGameManager.players = new Dictionary<int, PlayerManager>();
            ClassicGameManager.instance.SpawnPlayer(1, "username");
            Assert.True(ClassicGameManager.instance.enemyPlayer.activeSelf);
        }

        //ClasicGameManager test    
        [Test]
        public  void AddCardToHandClassicGameManager()
        {
            ClassicGameManager manager = GameObject.Find("GameManager").GetComponent<ClassicGameManager>();
            manager.Awake();
            ClassicGameManager.instance.AddCardToHand("Goblin");
            Assert.AreEqual("Goblin", ClassicGameManager.instance.handCanvas.transform.GetChild(0).name);
            ClassicGameManager.instance.handCanvas.transform.GetChild(0).SetParent(null); 
        }

        [Test]
        public  void PullStartingCardsClassicGameManager()
        {
            ClassicGameManager manager = GameObject.Find("GameManager").GetComponent<ClassicGameManager>();
            manager.Awake();
            foreach (Transform child in ClassicGameManager.instance.handCanvas.transform)
            {
                child.SetParent(null);
            }
            ClassicGameManager.instance.PullStartingCards("Goblin,Dwarf");
            Assert.AreEqual(2, ClassicGameManager.instance.handCanvas.transform.childCount);
        }

        [Test]
        public  void SetTurnTrueClassicGameManager()
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
            ClassicGameManager.instance.SpawnPlayer(2, "username");
            ClassicGameManager.instance.SetTurn(true);
            Assert.True(ClassicGameManager.curPlayer.isTurn);
        }
        [Test]
        public  void SetTurnFlaseClassicGameManager()
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
            ClassicGameManager.instance.SpawnPlayer(2, "username");
            ClassicGameManager.instance.SetTurn(false);
            Assert.False(ClassicGameManager.curPlayer.isTurn);
        }

        [Test]
        public  void SetLifeClassicGameManager()
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
            ClassicGameManager.instance.SpawnPlayer(2, "username");
            ClassicGameManager.instance.SetLife(2, 10);
            Assert.AreEqual(10, ClassicGameManager.curPlayer.life);
        }

        [Test]
        public  void SetManaClassicGameManager()
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
            ClassicGameManager.instance.SpawnPlayer(2, "username");
            ClassicGameManager.instance.SetMana(2, 10);
            Assert.AreEqual(10, ClassicGameManager.curPlayer.life);
        }

        [Test]
        public  void SetMaxManaClassicGameManager()
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
            ClassicGameManager.instance.SpawnPlayer(2, "username");
            ClassicGameManager.instance.SetMaxMana(2, 10);
            Assert.AreEqual(10, ClassicGameManager.curPlayer.life);
        }

        [Test]
        public  void SetDeckCountClassicGameManager()
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
            ClassicGameManager.instance.SpawnPlayer(2, "username");
            ClassicGameManager.instance.SetDeckCardCount(10);
            Assert.AreEqual("10", ClassicGameManager.instance.deckCount.text);
        }

        [Test]
        public  void PutOnTablePlayersClassicGameManager()
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
            ClassicGameManager.instance.PutOnTable("Rat", true);
            Assert.AreEqual("Rat", ClassicGameManager.instance.playerBoard.transform.GetChild(0).name);
        }

        [Test]
        public void PutOnTableEnemysClassicGameManager()
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
            foreach (Transform child in ClassicGameManager.instance.enemyBoard.transform)
            {
                child.SetParent(null);
            }
            ClassicGameManager.instance.SpawnPlayer(2, "username");
            ClassicGameManager.instance.PutOnTable("Rat", false);
            Assert.AreEqual("Rat", ClassicGameManager.instance.enemyBoard.transform.GetChild(0).name);
        }

        [Test]
        public void ReorginizeClassicGameManager()
        {
            ClassicGameManager manager = GameObject.Find("GameManager").GetComponent<ClassicGameManager>();
            manager.Awake();
            ClassicGameManager.instance.AddCardToHand("Goblin");
            Assert.AreEqual(ClassicGameManager.instance.handCanvas.transform.GetChild(0).localPosition.x + 100, ClassicGameManager.instance.handCanvas.transform.GetChild(1).localPosition.x);
        }

        //UiManager test
        [Test]
        public void ActivateTimerUiManager()
        {
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            UIManager.instance.ActivateTimer();
            Assert.True(UIManager.instance.timer.activeSelf);
        }

        [Test]
        public void ActivateEndTurnrUiManager()
        {
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            UIManager.instance.ActivateEndTurn();
            Assert.True(UIManager.instance.endTurn.activeSelf);
        }

        [Test]
        public void OnReturneTrueUiManager()
        {
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            UIManager.instance.pauze.SetActive(false);
            UIManager.instance.OnReturn();
            Assert.True(UIManager.instance.pauze.activeSelf);
        }

        [Test]
        public void OnReturneFalseUiManager()
        {
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            UIManager.instance.pauze.SetActive(true);
            UIManager.instance.OnReturn();
            Assert.False(UIManager.instance.pauze.activeSelf);
        }


        //Deck tests
        [Test]
        public void ToStringDeck()
        {
            List<DeckFormat> deckFormat = new List<DeckFormat>();
            deckFormat.Add(new DeckFormat("goblin", 2));

            Deck deck = new Deck(deckFormat);

            Assert.AreEqual("goblin,goblin", deck.ToString());
        }

        [Test]
        public void PullCardDeck()
        {
            List<DeckFormat> deckFormat = new List<DeckFormat>();
            deckFormat.Add(new DeckFormat("goblin", 2));

            Deck deck = new Deck(deckFormat);

            Assert.AreEqual("Goblin", deck.pullCard().name);
        }

        //Enemy Hand tests.
        [Test]
        public void AddCardEnemyHand()
        {
            GameObject.Find("GameManager").GetComponent<ClassicGameManager>().enemyPlayer.SetActive(true);

            EnemyHand enemyHand = GameObject.Find("/Canvas/Enemy/CardsInHand").GetComponent<EnemyHand>();
            enemyHand.Awake();
            EnemyHand.instance.AddCard();

            Assert.AreEqual(1, EnemyHand.instance.transform.childCount);
            enemyHand.transform.GetChild(0).SetParent(null);
        }

        [Test]
        public void SetCardCountEnemyHand()
        {
            GameObject.Find("GameManager").GetComponent<ClassicGameManager>().enemyPlayer.SetActive(true);

            EnemyHand enemyHand = GameObject.Find("/Canvas/Enemy/CardsInHand").GetComponent<EnemyHand>();
            enemyHand.Awake();
            EnemyHand.instance.SetCardCount(8);

            Assert.AreEqual(8, EnemyHand.instance.cardCount);
        }

        // Mana test in Puzzle
        [Test]
        public void useManaMana()
        {
            Mana mana = GameObject.Find("Canvas/Player").GetComponent<Mana>();
            mana.currentMana = 10;
            mana.maxMana = 10;

            mana.useMana(4);
            Assert.AreEqual(6, mana.currentMana);
        }

        [Test]
        public void canUseCardMana()
        {
            Mana mana = GameObject.Find("Canvas/Player").GetComponent<Mana>();
            mana.currentMana = 5;
            mana.maxMana = 10;

            bool result = mana.canUseCard(6);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void takeDamageHealth()
        {

            Health health = GameObject.Find("Canvas/Player").GetComponent<Health>();
            health.health = 30;

            health.takeDamage(6);
            Assert.AreEqual(24, health.health);
        }

        [Test]
        public void RestoreHealthHealth()
        {
            Health health = GameObject.Find("Canvas/Player").GetComponent<Health>();
            health.health = 5;

            health.restoreHealth(6);
            Assert.AreEqual(11, health.health);
        }
    }
}
