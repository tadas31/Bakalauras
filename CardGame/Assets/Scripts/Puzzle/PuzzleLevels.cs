using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Assertions.Must;

public class PuzzleLevels : MonoBehaviour
{
    private List<Puzzle> levels;        // All levels.

    public GameObject levelButton;      // Level button prefab.
    public Transform parent;            // Parent for all buttons.


    void OnEnable()
    {
        levels = Resources.LoadAll<Puzzle>("Puzzle/CreatedPuzzles").ToList();

        int completedLevels = SaveSystem.LoadCompletedPuzzles() != null ? SaveSystem.LoadCompletedPuzzles().completedPuzzles : 0;

        foreach (Puzzle puzzle in levels)
        {
            GameObject newButton = Instantiate(levelButton);
            newButton.transform.name = puzzle.name;
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = puzzle.name;
            newButton.transform.SetParent(parent, false);

            if (int.Parse(puzzle.name) > completedLevels + 1)
            {
                newButton.GetComponent<Button>().interactable = false;
            }

        }
    }

    void OnDisable()
    {
        foreach (Transform level in parent.GetComponentInChildren<Transform>())
            Destroy(level.gameObject);
    }

}
