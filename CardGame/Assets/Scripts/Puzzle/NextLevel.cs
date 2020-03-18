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
        nextLevel.SetActive(true);

        SelectedPuzzle.Level = SelectedPuzzle.Level + 1;
        Puzzle puzzle = Resources.Load<Puzzle>("Puzzle/CreatedPuzzles/" + SelectedPuzzle.Level);

        if (puzzle == null)
            nextLevelButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onNextLevelPress()
    {
        SceneManager.LoadScene("Puzzle");
    }

    public void onMenuPress()
    {
        SceneManager.LoadScene("Menu");
    }
}
