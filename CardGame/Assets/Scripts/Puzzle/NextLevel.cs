using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public GameObject nextLevel;
    public Button nextLevelButton;
    public LoadScene loadScene;

    // Start is called before the first frame update
    void Start()
    {
        SaveSystem.SaveCompletedPuzzles(SelectedPuzzle.Level);

        nextLevel.SetActive(true);

        // Sets next level
        SelectedPuzzle.Level = SelectedPuzzle.Level + 1;

        // Checks if next level exists
        Puzzle puzzle = Resources.Load<Puzzle>("Puzzle/CreatedPuzzles/" + SelectedPuzzle.Level);
        if (puzzle == null)
            nextLevelButton.interactable = false;
    }


    /// <summary>
    /// Moves to next level
    /// </summary>
    public void onNextLevelPress()
    {
        loadScene.LoadNewScene("Puzzle");
    }

    /// <summary>
    /// Goes back to menu
    /// </summary>
    public void onMenuPress()
    {
        loadScene.LoadNewScene("Menu");
    }
}
