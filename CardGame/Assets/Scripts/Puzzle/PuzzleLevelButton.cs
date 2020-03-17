using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleLevelButton : MonoBehaviour
{
    // Loads Level on button press
    public void onLevelPress()
    {
        int selectedPuzzle = int.Parse(transform.name);

        Puzzle puzzle = Resources.Load<Puzzle>("Puzzle/CreatedPuzzles/" + selectedPuzzle);

        // Get's script type.
        System.Type scriptType = System.Type.GetType(puzzle.winCondition + ",Assembly-CSharp");
        if (scriptType != null)
        {
            SelectedPuzzle.Level = selectedPuzzle;
            SceneManager.LoadScene("Puzzle");
        }
        else
        {
            Debug.Log("Failed to load level win condition not found");
            return;
        }
    }
}
