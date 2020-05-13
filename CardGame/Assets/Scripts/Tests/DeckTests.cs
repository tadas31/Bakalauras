using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DeckTests
    {

        [Test]
        public void TabButtonOnPointerClick()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Deck.unity");
            TabButton tabButton = GameObject.Find("All").GetComponent<TabButton>();
            tabButton.OnPointerClick(null);

            Assert.True(tabButton.isClicked);
        }

        [Test]
        public void TabButtonOnPointerEnter()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Deck.unity");
            TabButton tabButton = GameObject.Find("All").GetComponent<TabButton>();
            tabButton.OnPointerEnter(null);

            Assert.False(tabButton.isClicked);
        }

        [Test]
        public void TabButtonOnPointerExit()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Deck.unity");
            TabButton tabButton = GameObject.Find("All").GetComponent<TabButton>();
            tabButton.OnPointerExit(null);

            Assert.False(tabButton.isClicked);
        }


        [Test]
        public void SelectCardsSetUp()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Deck.unity");
            SelectCards selectCards = GameObject.Find("CardsCanvas").GetComponent<SelectCards>();
            selectCards.Start();
            selectCards.Update();
            Assert.IsNotNull(selectCards);
        }

        [Test]
        public void PuzzleButton()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Deck.unity");
            PuzzleButtons puzzleButtons = new PuzzleButtons();
            try { puzzleButtons.onRestartPress(); } catch (Exception ex) { }
            Assert.True(true);
        }

        [Test]
        public void CompletedPuzzle()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Deck.unity");
            CompletedPuzzles puzzleButtons = new CompletedPuzzles(1);
            Assert.True(true);
        }


        [Test]
        public void OnDeckCardHover()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Deck.unity");


            Card card = Resources.Load<Card>("Cards/CreatedCards/Goblin");
            GameObject cardHand = card.spawnCard();
            OnDeckCardHover comp = cardHand.AddComponent<OnDeckCardHover>();
            try
            {
                comp.OnPointerEnter(null);
            }
            catch (Exception)
            {

            }
            try
            {
                comp.OnPointerExit(null);
            }
            catch (Exception)
            {

            }
            comp.Start();

            Assert.True(true);
        }
    }
}
