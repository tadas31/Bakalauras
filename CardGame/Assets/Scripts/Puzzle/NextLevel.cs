using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public GameObject nextLevel;
    public Button nextLevelButton;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Moves to next level
    /// </summary>
    public void onNextLevelPress()
    {
        SceneManager.LoadScene("Puzzle");
    }

    /// <summary>
    /// Goes back to menu
    /// </summary>
    public void onMenuPress()
    {
        SceneManager.LoadScene("Menu");
    }
}
