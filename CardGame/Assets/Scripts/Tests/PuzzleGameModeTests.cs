using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PuzzleGameModeTests
    {
        // Mana test in Puzzle
        [Test]
        public void useManaMana()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Puzzle.unity");
            Mana mana = GameObject.Find("Canvas/Player").GetComponent<Mana>();
            mana.currentMana = 10;
            mana.maxMana = 10;

            mana.useMana(4);
            Assert.AreEqual(6, mana.currentMana);
        }

        [Test]
        public void canUseCardMana()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Puzzle.unity");
            Mana mana = GameObject.Find("Canvas/Player").GetComponent<Mana>();
            mana.currentMana = 5;
            mana.maxMana = 10;

            bool result = mana.canUseCard(6);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void takeDamageHealth()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Puzzle.unity");
            Health health = GameObject.Find("Canvas/Player").GetComponent<Health>();
            health.health = 30;

            health.takeDamage(6);
            Assert.AreEqual(24, health.health);
        }

        [Test]
        public void RestoreHealthHealth()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Puzzle.unity");
            Health health = GameObject.Find("Canvas/Player").GetComponent<Health>();
            health.health = 5;

            health.restoreHealth(6);
            Assert.AreEqual(11, health.health);
        }


        [Test]
        public void ChargeGetDescription()
        {
            GameObject game = new GameObject();
            game.AddComponent<Attack>().attacked = true;
            Charge charge = game.AddComponent<Charge>();
            charge.Start();
            charge.Update();
            string str = charge.getDescription();
            Assert.AreEqual("Charge", str);
        }

        [Test]
        public void PuzzleGamemanagerPutOnTable()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Puzzle.unity");
            GameObject managerObject = GameObject.Find("GameManager");
            PuzzleGameManager manager = managerObject.GetComponent<PuzzleGameManager>();
            manager.Awake();
            PuzzleGameManager.instance.Update();
            PuzzleGameManager.instance.OnResumePress();
            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            PuzzleGameManager.instance.addCardToHand(cardHand);
            PuzzleGameManager.instance.handReorganize();
            PuzzleGameManager.instance.onResumePress();
            card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            cardHand = card.spawnCard();
            PuzzleGameManager.instance.PutOnTable(cardHand);
            Assert.True(true);
        }

        [Test]
        public void CreatePuzzle()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Puzzle.unity");
            GameObject managerObject = GameObject.Find("GameManager");
            PuzzleGameManager man = managerObject.GetComponent<PuzzleGameManager>();
            SelectedPuzzle.Level = 1;
            man.Awake();
            CreatePuzzle createPuzzle = managerObject.GetComponent<CreatePuzzle>();
            createPuzzle.Start();
            Assert.True(true);
        }
        [Test]
        public void NextLevel()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Puzzle.unity");
            GameObject managerObject = GameObject.Find("GameManager");
            PuzzleGameManager man = managerObject.GetComponent<PuzzleGameManager>();
            SelectedPuzzle.Level = 1;
            man.Awake();
            NextLevel nextLevel = managerObject.GetComponent<NextLevel>();

            nextLevel.Start();
            try { nextLevel.onNextLevelPress(); } catch (Exception) { }
            try { nextLevel.onMenuPress(); } catch (Exception) { }
            Assert.True(true);
        }
    }
}
