using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class MenuTests
    {

        [Test]
        public void MenuButtonsOnClassicPress()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity");
            MenuButtons menu = GameObject.Find("MenuButtons").GetComponent<MenuButtons>();
            menu.onClassicPress();
            Scene scene = SceneManager.GetActiveScene();

            Assert.AreEqual("Menu", scene.name);
        }

        [Test]
        public void MenuButtonsOnPuzzlePress()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity");
            MenuButtons menu = GameObject.Find("MenuButtons").GetComponent<MenuButtons>();
            menu.onPuzzlePerss();
            Scene scene = SceneManager.GetActiveScene();

            Assert.AreEqual("Menu", scene.name);
        }

        [Test]
        public void MenuButtonsOnDeckPress()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity");
            MenuButtons menu = GameObject.Find("MenuButtons").GetComponent<MenuButtons>();
            menu.onDeckPress();
            Scene scene = SceneManager.GetActiveScene();

            Assert.AreEqual("Menu", scene.name);
        }

        [Test]
        public void MenuButtonsOnOptionPress()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity");
            MenuButtons menu = GameObject.Find("MenuButtons").GetComponent<MenuButtons>();
            menu.optionsScript.Start();
            menu.onOptionsPress();
            SoundVolume soundVolume = GameObject.Find("Music").GetComponent<SoundVolume>();
            soundVolume.onIncreasePress();
            soundVolume.onDecreasePress();
            Scene scene = SceneManager.GetActiveScene();


            Assert.AreEqual("Menu", scene.name);
        }

        [Test]
        public void MenuButtonsOnonQuitPress()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity");
            MenuButtons menu = GameObject.Find("MenuButtons").GetComponent<MenuButtons>();
            menu.onQuitPress();
            Scene scene = SceneManager.GetActiveScene();

            Assert.AreEqual("Menu", scene.name);
        }

        [Test]
        public void OptionsOnApplyPress()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity");
            MenuButtons menu = GameObject.Find("MenuButtons").GetComponent<MenuButtons>();
            menu.optionsScript.Start();
            menu.onOptionsPress();
            menu.optionsScript.windowModeToggle.isOn = true;
            menu.optionsScript.onApplyPress();
            Scene scene = SceneManager.GetActiveScene();


            Assert.True(menu.optionsScript.windowModeToggle.isOn);
        }

        [Test]
        public void OptionOnDeckResetPress()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity");
            MenuButtons menu = GameObject.Find("MenuButtons").GetComponent<MenuButtons>();
            menu.optionsScript.Start();
            menu.onOptionsPress();
            menu.optionsScript.windowModeToggle.isOn = true;
            menu.optionsScript.OnDeckResetPress();

            Assert.True(menu.optionsScript.windowModeToggle.isOn);
        }

        [Test]
        public void OptionOnLevelResetPress()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity");
            MenuButtons menu = GameObject.Find("MenuButtons").GetComponent<MenuButtons>();
            menu.optionsScript.Start();
            menu.onOptionsPress();
            menu.optionsScript.windowModeToggle.isOn = true;
            menu.optionsScript.OnLevelResetPress();

            Assert.True(menu.optionsScript.windowModeToggle.isOn);
        }


        [Test]
        public void OptionOnResetCancellPress()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity");
            MenuButtons menu = GameObject.Find("MenuButtons").GetComponent<MenuButtons>();
            menu.optionsScript.Start();
            menu.onOptionsPress();
            menu.optionsScript.windowModeToggle.isOn = true;
            menu.optionsScript.OnResetCancellPress();

            Assert.True(menu.optionsScript.windowModeToggle.isOn);
        }

        [Test]
        public void SelectLevel()
        {
            SelectedPuzzle.Level = 1;

            Assert.AreEqual(1, SelectedPuzzle.Level);
        }

        [Test]
        public void ButtonAnimation()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity");
            ButtonAnimation buttonAnimation = GameObject.Find("MenuButtons").GetComponent<ButtonAnimation>();

            buttonAnimation.Start();
            buttonAnimation.Update();
            buttonAnimation.onPlayPress();
            buttonAnimation.Update();
            buttonAnimation.onPlayPress();
            Assert.True(true);
        }
    }
}
