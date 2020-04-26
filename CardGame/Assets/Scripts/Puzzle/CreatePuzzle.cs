using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreatePuzzle : MonoBehaviour
{
    private Puzzle puzzle;              // Puzzle to load.
    public GameObject enemyBoard;       // Parent to enemy cards.
    public Health playerHealth;         // Reference to health class for player.
    public Health enemyHealth;          // Reference to health class for enemy.
    public Mana playerMana;             // Reference to mana class for player.

    // Start is called before the first frame update
    void Start()
    {
        // Get's selected level
        puzzle = Resources.Load<Puzzle>("Puzzle/CreatedPuzzles/" + SelectedPuzzle.Level);

        // Set player health.
        playerHealth.health = puzzle.playerLife;
        playerHealth.displayHealth();

        // Set's player mana.
        playerMana.maxMana = puzzle.playerMana;
        playerMana.currentMana = puzzle.playerMana;
        playerMana.displayMana();

        // Set enemy health.
        enemyHealth.health = puzzle.enemyLife;
        enemyHealth.displayHealth();

        // Places enemy cards
        foreach (string card in puzzle.enemyCards)
        {
            Card c = Resources.Load<Card>("Cards/CreatedCards/" + card);
            GameObject newCard = c.spawnCard();
            newCard.transform.SetParent(enemyBoard.transform);
            newCard.GetComponent<CardStatsHelper>().enabled = true;
            newCard.transform.localScale = Vector3.one;
        }

        // Gets user cards
        foreach (string card in puzzle.playerCards)
        {
            Card c = Resources.Load<Card>("Cards/CreatedCards/" + card);
            GameObject newCard = c.spawnCard();
            GetComponent<PuzzleGameManager>().addCardToHand(newCard);
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
