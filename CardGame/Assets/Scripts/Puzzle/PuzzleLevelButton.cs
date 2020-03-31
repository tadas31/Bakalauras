using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleLevelButton : MonoBehaviour
{
    private LoadScene loadScene;

    // Start is called before the first frame update
    void Start()
    {
        loadScene = GameObject.Find("Transition").GetComponent<LoadScene>();
    }

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
            loadScene.LoadNewScene("Puzzle");
        }
        else
        {
            Debug.Log("Failed to load level win condition not found");
            return;
        }
    }
}
