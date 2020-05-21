using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlecryHealthForTaunt : MonoBehaviour, IDescription, ISpellDamage
{
    private AttackHelper attackHelper;      // Reference to attack helper script.
    private int healthToGain;               // Amount of health card will gain for each taunt on board.

    private void OnEnable()
    {
        if (transform.parent != null && transform.parent.name.ToLower().Contains("board"))
        {
            attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();

            // Gets all cards on board.
            List<Transform> allCards = attackHelper.getAllEnemyCards().Concat(attackHelper.getAllPlayerCards()).ToList();

            // Counts all taunts on board.
            int tauntsCount = 0;
            foreach(Transform card in allCards)
            {
                if (card.parent.GetComponent<Taunt>() != null)
                    tauntsCount++;
            }

            // If there are taunts on board increase cards health.
            if (tauntsCount > 0)
            {
                CardStatsHelper cardstatsHelper = GetComponent<CardStatsHelper>();
                cardstatsHelper.takeDamage(-tauntsCount * healthToGain);
            }

        }
    }

    // Returns description of effect granted by this script.
    public string getDescription()
    {
        string description = "Battlecry gain " + healthToGain + " health for each Taunt on board";
        return description;
    }

    // Sets amount of health card will gain for each taunt on board.
    public void setSpellDamage(int spellDamage)
    {
        healthToGain = spellDamage;
    }
}
