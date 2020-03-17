using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreatePuzzle : MonoBehaviour
{
    private Puzzle puzzle;              // Puzzle to load.
    public GameObject enemyBoard;       // Parent to enemy cards.
    public GameObject handCanvas;       // Parent to player cards.

    // Start is called before the first frame update
    void Start()
    {
        // Get's selected level
        puzzle = Resources.Load<Puzzle>("Puzzle/CreatedPuzzles/" + SelectedPuzzle.Level);


        foreach (string card in puzzle.enemyCards)
        {
            Card c = Resources.Load<Card>("Cards/CreatedCards/" + card);
            GameObject newCard = c.spawnCard();
            newCard.transform.SetParent(enemyBoard.transform);
            newCard.GetComponent<CardStatsHelper>().enabled = true;
            newCard.transform.localScale = Vector3.one;
        }

        foreach (string card in puzzle.playerCards)
        {
            Card c = Resources.Load<Card>("Cards/CreatedCards/" + card);
            GameObject newCard = c.spawnCard();
            GetComponent<GameManager>().addCardToHand(newCard);
        }

        // Get's script type and adds it to game manager.
        System.Type scriptType = System.Type.GetType(puzzle.winCondition + ",Assembly-CSharp");
        gameObject.AddComponent(scriptType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
