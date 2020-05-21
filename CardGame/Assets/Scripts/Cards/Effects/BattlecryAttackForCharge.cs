using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlecryAttackForCharge : MonoBehaviour, IDescription, ISpellDamage
{
    private AttackHelper attackHelper;      // Reference to attack helper script.
    private int attackToGain;               // Amount of attack card will gain for each charge on board.

    
    private void OnEnable()
    {
        if (transform.parent != null && transform.parent.name.ToLower().Contains("board"))
        {
            attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();

            // Gets all cards on board.
            List<Transform> allCards = attackHelper.getAllEnemyCards().Concat(attackHelper.getAllPlayerCards()).ToList();

            // Counts all charges on board.
            int chargesCount = 0;
            foreach(Transform card in allCards)
            {
                if (card.parent.GetComponent<Charge>() != null)
                    chargesCount++;
            }

            // IF there are charges on board increases cards attack.
            if (chargesCount > 0)
            {
                CardStatsHelper cardstatsHelper = GetComponent<CardStatsHelper>();
                cardstatsHelper.setAttack(cardstatsHelper.getAttack() + chargesCount * attackToGain);
            }
        }
    }

    // Returns description of effect granted by this script.
    public string getDescription()
    {
        string description = "Battlecry gain " + attackToGain + " attack for each Charge on board";
        return description;
    }

    // Sets amount of attack card will gain for each charge on board.
    public void setSpellDamage(int spellDamage)
    {
        attackToGain = spellDamage;
    }
}
