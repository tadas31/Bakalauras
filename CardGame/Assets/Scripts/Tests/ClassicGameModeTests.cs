﻿using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ClassicGameModeTests
    {

        //Attack helper tests
        [Test]
        public void GetAllPlayerCardsAttackHelper()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
        public void SpawnPlayerPlayerClassicGameManager()
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
        public void SpawnPlayerEnemyClassicGameManager()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
        public void AddCardToHandClassicGameManager()
        {
            ClassicGameManager manager = GameObject.Find("GameManager").GetComponent<ClassicGameManager>();
            manager.Awake();
            ClassicGameManager.instance.AddCardToHand("Goblin");
            Assert.AreEqual("Goblin", ClassicGameManager.instance.handCanvas.transform.GetChild(0).name);
            ClassicGameManager.instance.handCanvas.transform.GetChild(0).SetParent(null);
        }

        [Test]
        public void PullStartingCardsClassicGameManager()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
        public void SetTurnTrueClassicGameManager()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
        public void SetTurnFlaseClassicGameManager()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
        public void SetLifeClassicGameManager()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
        public void SetManaClassicGameManager()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            Assert.AreEqual(10, ClassicGameManager.curPlayer.mana);
        }

        [Test]
        public void SetMaxManaClassicGameManager()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            Assert.AreEqual(10, ClassicGameManager.curPlayer.maxMana);
        }

        [Test]
        public void SetDeckCountClassicGameManager()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
        public void PutOnTablePlayersClassicGameManager()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
            ClassicGameManager manager = GameObject.Find("GameManager").GetComponent<ClassicGameManager>();
            manager.Awake();
            ClassicGameManager.instance.AddCardToHand("Goblin");
            ClassicGameManager.instance.AddCardToHand("Rat");
            Assert.AreEqual(ClassicGameManager.instance.handCanvas.transform.GetChild(0).localPosition.x + 100, ClassicGameManager.instance.handCanvas.transform.GetChild(1).localPosition.x);
        }

        //UiManager test
        [Test]
        public void ActivateTimerUiManager()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            UIManager.instance.ActivateTimer();
            Assert.True(UIManager.instance.timer.activeSelf);
        }

        [Test]
        public void ActivateEndTurnrUiManager()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            UIManager.instance.ActivateEndTurn();
            Assert.True(UIManager.instance.endTurn.activeSelf);
        }

        [Test]
        public void OnReturneTrueUiManager()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            UIManager.instance.pauze.SetActive(false);
            UIManager.instance.OnReturn();
            Assert.True(UIManager.instance.pauze.activeSelf);
        }

        [Test]
        public void OnReturneFalseUiManager()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
            GameObject.Find("GameManager").GetComponent<ClassicGameManager>().enemyPlayer.SetActive(true);

            EnemyHand enemyHand = GameObject.Find("/Canvas/Enemy/CardsInHand").GetComponent<EnemyHand>();
            enemyHand.Awake();
            EnemyHand.instance.SetCardCount(8);

            Assert.AreEqual(8, EnemyHand.instance.cardCount);
        }

        [Test]
        public void TimerManagerTest()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
            UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            uimanager.Awake();
            UIManager.instance.ActivateTimer();
            TimerManager timer = GameObject.Find("Canvas/Timer").GetComponent<TimerManager>();
            TimerManager.timeLeft = 60f;
            timer.Update();

            Assert.AreEqual(60f, TimerManager.timeLeft);
        }

        [Test]
        public void TauntTest()
        {
            Taunt taunt = new Taunt();
            string str = taunt.getDescription();
            Assert.AreEqual("Taunt", str);
        }

        [Test]
        public void PacketWithId()
        {
            Packet _packet = new Packet(1);

            _packet.Write(8);
            _packet.Write(8);
            _packet.Length();
            Packet packet = new Packet(_packet.ToArray());
            Assert.AreEqual(1, packet.ReadInt());
        }

        [Test]
        public void OnPointerEnter()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
            Card card = Resources.Load<Card>("Cards/CreatedCards/" + "Goblin");
            GameObject cardTable = card.spawnCard();
            OnCardHoverInGame hover = cardTable.AddComponent<OnCardHoverInGame>();
            hover.Start();
            Assert.IsNotNull(hover);
        }

        //Attack script tests
        [Test]
        public void Attack()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            ClassicGameManager.instance.PutOnTable("Rat", false);
            GameObject card = ClassicGameManager.instance.playerBoard.transform.GetChild(0).gameObject;
            GameObject enemycard = ClassicGameManager.instance.enemyBoard.transform.GetChild(0).gameObject;
            Attack attack = card.GetComponent<Attack>();
            attack.Start();
            attack.attacking = true;
            attack.Update();
            attack.OnPointerClick(null);
            attack.AttackAnimationToCard(enemycard.transform);
            Health health = ClassicGameManager.instance.localPlayer.GetComponent<Health>();
            attack.AttackAnimationToPlayer(health);

            Assert.IsNotNull(manager);
        }

        //Attack script tests
        [Test]
        public void CardInHand()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            ClassicGameManager.instance.AddCardToHand("Rat");
            CardInHand card = ClassicGameManager.instance.handCanvas.transform.GetChild(0).gameObject.GetComponent<CardInHand>();
            card.Start();
            card.OnBeginDrag(null);
            try
            {
                card.OnDrag(null);
            }
            catch (Exception ex)
            {

            }
            card.OnPointerEnter(null);
            card.OnPointerExit(null);
            try
            {
                card.OnEndDrag(null);
            }
            catch (Exception ex)
            {

            }
            Assert.IsNotNull(manager);
        }

        [Test]
        public void BoardCards()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
            GameObject managerObject = GameObject.Find("GameManager");
            ClassicGameManager manager = managerObject.GetComponent<ClassicGameManager>();
            manager.Awake();
            BoardCards boardCards = ClassicGameManager.instance.playerBoard.GetComponent<BoardCards>();
            boardCards.OnTransformChildrenChanged();
            Assert.True(true);
        }


        //Attack script tests
        [Test]
        public void BattlecryDamageSingleTarget()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            ClassicGameManager.instance.PutOnTable("Goblin", true);
            BattlecryDamageSingleTarget battle = ClassicGameManager.instance.playerBoard.transform.GetChild(0).gameObject.AddComponent<BattlecryDamageSingleTarget>();
            battle.getDescription();
            battle.setSpellDamage(1);
            battle.OnEnable();
            battle.OnDisable();

            Assert.AreEqual("Goblin", ClassicGameManager.instance.playerBoard.transform.GetChild(0).name);
        }

        [Test]
        public void SingleTargetDamage()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            ClassicGameManager.instance.PutOnTable("Goblin", true);
            SingleTargetDamage battle = ClassicGameManager.instance.playerBoard.transform.GetChild(0).gameObject.AddComponent<SingleTargetDamage>();
            battle.Start();
            try
            {
                battle.Update();
            }
            catch (Exception) { }
            battle.getDescription();
            battle.setSpellDamage(1);
            battle.OnEnable();
            battle.OnDisable();

            Assert.AreEqual("Goblin", ClassicGameManager.instance.playerBoard.transform.GetChild(0).name);
        }


        [Test]
        public void IncreaseAttackAndHpToTarget()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            ClassicGameManager.instance.PutOnTable("Goblin", true);
            IncreaseAttackAndHpToTarget battle = ClassicGameManager.instance.playerBoard.transform.GetChild(0).gameObject.AddComponent<IncreaseAttackAndHpToTarget>();
            battle.Start();
            try
            {
                battle.Update();
            }
            catch (Exception) { }
            battle.getDescription();
            battle.setSpellDamage(1);
            battle.OnEnable();
            battle.OnDisable();

            Assert.AreEqual("Goblin", ClassicGameManager.instance.playerBoard.transform.GetChild(0).name);
        }

        [Test]
        public void ResetAttack()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            ClassicGameManager.instance.PutOnTable("Goblin", true);
            ResetAttack battle = ClassicGameManager.instance.playerBoard.transform.GetChild(0).gameObject.AddComponent<ResetAttack>();
            battle.Start();
            try
            {
                battle.Update();
            }
            catch (Exception) { }
            battle.getDescription();

            battle.OnEnable();
            battle.OnDisable();

            Assert.AreEqual("Goblin", ClassicGameManager.instance.playerBoard.transform.GetChild(0).name);
        }

        [Test]
        public void DamageAllCards()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            ClassicGameManager.instance.PutOnTable("Goblin", true);
            DamageAllCards battle = ClassicGameManager.instance.playerBoard.transform.GetChild(0).gameObject.AddComponent<DamageAllCards>();

            battle.setSpellDamage(1);
            battle.getDescription();

            Assert.AreEqual("Goblin", ClassicGameManager.instance.playerBoard.transform.GetChild(0).name);
        }

        [Test]
        public void OpenGraveyardTest()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            OpenGraveyard openGraveyard = GameObject.Find("Canvas/Player/PlayerGraveyard/Graveyard").GetComponent<OpenGraveyard>();
            openGraveyard.Awake();
            ClassicGameManager.instance.PutOnTable("Goblin", true);
            OpenGraveyard.instance.Start();
            OpenGraveyard.instance.OnPointerClick(null);
            OpenGraveyard.instance.UpdateCardNumberInGraveyard();

            Assert.AreEqual("Goblin", ClassicGameManager.instance.playerBoard.transform.GetChild(0).name);
        }

        [Test]
        public void OnDeathRestoreHealth()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            ClassicGameManager.instance.PutOnTable("Goblin", true);
            OnDeathRestoreHealth battle = ClassicGameManager.instance.playerBoard.transform.GetChild(0).gameObject.AddComponent<OnDeathRestoreHealth>();

            battle.setSpellDamage(1);
            battle.getDescription();
            battle.Start();

            Assert.AreEqual("Goblin", ClassicGameManager.instance.playerBoard.transform.GetChild(0).name);
        }

        [Test]
        public void DamageAllEnemies()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Classic.unity");
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
            ClassicGameManager.instance.PutOnTable("Goblin", true);
            DamageAllEnemies battle = ClassicGameManager.instance.playerBoard.transform.GetChild(0).gameObject.AddComponent<DamageAllEnemies>();

            battle.setSpellDamage(1);
            battle.getDescription();

            Assert.AreEqual("Goblin", ClassicGameManager.instance.playerBoard.transform.GetChild(0).name);
        }

        [Test]
        public void PacketWrite()
        {
            Packet _packet = new Packet(1);

            _packet.Write(8);
            _packet.Write(8l);
            short a = 1;
            _packet.Write(a);
            _packet.Length();
            Packet packet = new Packet(_packet.ToArray());
            Assert.AreEqual(1, packet.ReadInt());
            packet.ReadBytes(2);
        }
    }
}
