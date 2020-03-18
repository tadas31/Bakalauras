using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllEnemyCards : MonoBehaviour, IWinCondition
{
    private NextLevel nextLevel;        // Reference to next level script.
    private AttackHelper attackHelper;  // Reference to attackHelper script.
    private bool won;                   // If true user has cleared the level.


    // Start is called before the first frame update
    void Start()
    {
        nextLevel = GetComponent<NextLevel>();
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();
        won = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackHelper.getAllEnemyCards().Count == 0 && !won)
        {
            won = true;
            nextLevel.enabled = true;
        }
    }

    /// <summary>
    /// returns win condition
    /// </summary>
    /// <returns> win condition </returns>
    public string winCondition()
    {
        return "Destroy all enemy cards in one turn";
    }
}
