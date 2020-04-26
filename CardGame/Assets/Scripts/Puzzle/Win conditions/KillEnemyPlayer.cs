using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyPlayer : MonoBehaviour, IWinCondition
{
    private NextLevel nextLevel;        // Reference to next level script.
    private Health enemyHealth;         // Reference to enemy health script.
    private bool won;                   // If true user has cleared the level.

    // Start is called before the first frame update
    void Start()
    {
        nextLevel = GetComponent<NextLevel>();
        enemyHealth = GameObject.Find("Canvas/Enemy").GetComponent<Health>();
        won = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.health <= 0 && !won)
        {
            won = true;
            nextLevel.enabled = true;
        }
    }

    /// <summary>
    /// Returns win condition.
    /// </summary>
    /// <returns> Win condition. </returns>
    public string winCondition()
    {
        return "Kill enemy player in one turn";
    }
}
