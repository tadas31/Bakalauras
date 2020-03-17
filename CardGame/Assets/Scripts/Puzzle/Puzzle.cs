using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle", menuName = "New Puzzle")]
public class Puzzle : ScriptableObject
{
    public string winCondition;             // Script that specifies win condition and checks if it has been met.
    public int enemyLife;                   // Enemy starting life.
    public int playerLife;                  // Player starting life.
    public int playerMana;                  // Player starting mana.

    public List<string> enemyCards;         // Enemy cards.
    public List<string> playerCards;        // Player cards.
    

}
