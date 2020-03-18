﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleLevels : MonoBehaviour
{
    private List<Puzzle> levels;        // All levels.

    public GameObject levelButton;      // Level button prefab.
    public Transform parent;            // Parent for all buttons.
    // Start is called before the first frame update
    void Start()
    {
        levels = Resources.LoadAll<Puzzle>("Puzzle/CreatedPuzzles").ToList();

        int completedLevels = SaveSystem.LoadCompletedPuzzles() != null ? SaveSystem.LoadCompletedPuzzles().completedPuzzles : 0;

        foreach (Puzzle puzzle in levels)
        {
            GameObject newButton = Instantiate(levelButton);
            newButton.transform.name = puzzle.name;
            newButton.GetComponentInChildren<Text>().text = puzzle.name;
            newButton.transform.SetParent(parent, false);

            if (int.Parse(puzzle.name) > completedLevels + 1)
            {
                newButton.GetComponent<Button>().interactable = false;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
